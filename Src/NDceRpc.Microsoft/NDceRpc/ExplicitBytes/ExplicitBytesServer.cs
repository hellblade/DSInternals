using System;
using System.Runtime.InteropServices;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc.ExplicitBytes
{
	/// <summary>
	/// Provides server-side services for RPC
	/// </summary>
	public class ExplicitBytesServer : Server, IExplicitBytesServer
	{
		/// <summary> The interface Id the service is using </summary>
		public readonly Guid IID;

		private RpcExecuteHandler _handler;

		/// <summary>
		/// Allows a single subscription to this event to handle incomming requests rather than 
		/// deriving from and overriding the Execute call.
		/// </summary>
		public event RpcExecuteHandler OnExecute
		{
			add
			{
				lock (this)
				{
					Guard.Assert<InvalidOperationException>(_handler == null, "The interface id is already registered.");
					_handler = value;
				}
			}
			remove
			{
				lock (this)
				{
					Guard.NotNull(value);
					if (_handler != null)
					{
						Guard.Assert<InvalidOperationException>(object.ReferenceEquals(_handler.Target, value.Target) && object.ReferenceEquals(_handler.Method, value.Method));
					}
					_handler = null;
				}
			}
		}

		public override void Dispose()
		{
			_handler = null;
			base.Dispose();
		}

		/// <summary>
		/// Constructs an RPC server for the given interface guid, the guid is used to identify multiple rpc
		/// servers/services within a single process.
		/// </summary>
		public ExplicitBytesServer(Guid iid)
		{
			IID = iid;
			RpcTrace.Verbose("ServerRegisterInterface({0})", iid);
			Ptr<RPC_SERVER_INTERFACE> sIf = ServerInterfaceFactory.Create(_handle, iid, RpcRuntime.TYPE_FORMAT, RpcRuntime.FUNC_FORMAT, RpcEntryPoint);
			ServerRegisterInterface(sIf.Handle, _handle);
		}

		private uint RpcEntryPoint(IntPtr clientHandle, uint szInput, IntPtr input, out uint szOutput, out IntPtr output)
		{
			output = IntPtr.Zero;
			szOutput = 0u;
			try
			{
				byte[] bytesIn = new byte[szInput];
				Marshal.Copy(input, bytesIn, 0, bytesIn.Length);
				byte[] bytesOut;
				using (RpcCallInfo call = new RpcCallInfo(clientHandle))
				{
					bytesOut = Execute(call, bytesIn);
				}
				if (bytesOut == null)
				{
					return 1715u;
				}
				szOutput = (uint)bytesOut.Length;
				output = RpcRuntime.Alloc(szOutput);
				Marshal.Copy(bytesOut, 0, output, bytesOut.Length);
				return 0u;
			}
			catch (Exception ex)
			{
				RpcRuntime.Free(output);
				output = IntPtr.Zero;
				szOutput = 0u;
				RpcTrace.Error(ex);
				return 2147500037u;
			}
		}

		/// <summary>
		/// Can be over-ridden in a derived class to handle the incomming RPC request, or you can
		/// subscribe to the OnExecute event.
		/// </summary>
		public virtual byte[] Execute(IRpcCallInfo call, byte[] input)
		{
			return _handler?.Invoke(call, input);
		}
	}
}
