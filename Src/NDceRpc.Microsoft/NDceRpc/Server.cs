using System;
using System.Diagnostics;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc
{
	public class Server : IDisposable
	{
		private class RpcServerHandle : RpcHandle
		{
			protected override void DisposeHandle(ref IntPtr handle)
			{
				if (handle != IntPtr.Zero)
				{
					NativeMethods.RpcServerUnregisterIf(handle, IntPtr.Zero, 1u);
					handle = IntPtr.Zero;
				}
			}
		}

		/// <summary> The max limit of in-flight calls </summary>
		public const int MAX_CALL_LIMIT = 255;

		protected readonly RpcHandle _handle;

		protected static readonly UsageCounter _listenerCount = new UsageCounter("RpcRuntime.Listener.{0}", Process.GetCurrentProcess().Id);

		protected bool _isListening;

		protected uint _maxCalls;

		public Server()
		{
			_maxCalls = 255u;
			_handle = new RpcServerHandle();
		}

		/// <summary>
		/// Disposes of the server and stops listening if the server is currently listening
		/// </summary>
		public virtual void Dispose()
		{
			StopListening();
			_handle.Dispose();
		}

		protected void ServerRegisterInterface(IntPtr sIfHandle, RpcHandle handle)
		{
			ServerRegisterInterface(sIfHandle, handle, IntPtr.Zero, IntPtr.Zero);
		}

		protected void ServerRegisterInterface(IntPtr sIfHandle, RpcHandle handle, IntPtr mgrTypeUuid, IntPtr mgrEpv)
		{
			Guard.Assert(NativeMethods.RpcServerRegisterIf(sIfHandle, mgrTypeUuid, mgrEpv));
			handle.Handle = sIfHandle;
		}

		/// <summary>
		/// Used to ensure that the server is listening with a specific protocol type.  
		/// </summary>
		public void AddProtocol(RpcProtseq protocol, string endpoint, uint maxCalls)
		{
			serverUseProtseqEp(protocol, maxCalls, endpoint);
			_maxCalls = Math.Max(_maxCalls, maxCalls);
		}

		/// <summary>
		/// Adds a type of authentication sequence that will be allowed for RPC connections to this process.
		/// </summary>
		public bool AddAuthentication(RPC_C_AUTHN type)
		{
			return AddAuthentication(type, null);
		}

		/// <summary>
		/// Adds a type of authentication sequence that will be allowed for RPC connections to this process.
		/// </summary>
		public bool AddAuthentication(RPC_C_AUTHN type, string serverPrincipalName)
		{
			return serverRegisterAuthInfo(type, serverPrincipalName);
		}

		/// <summary>
		/// Starts the RPC listener for this instance,
		/// </summary>
		public void StartListening()
		{
			if (!_isListening)
			{
				_listenerCount.Increment(serverListen, _maxCalls);
				_isListening = true;
			}
		}

		/// <summary>
		/// Stops listening for this instance.
		/// </summary>
		public void StopListening()
		{
			if (_isListening)
			{
				_isListening = false;
				_listenerCount.Decrement(serverStopListening);
			}
		}

		private static void serverUseProtseqEp(RpcProtseq protocol, uint maxCalls, string endpoint)
		{
			RpcTrace.Verbose("serverUseProtseqEp({0})", protocol);
			RPC_STATUS err = NativeMethods.RpcServerUseProtseqEp(protocol.ToString(), maxCalls, endpoint, IntPtr.Zero);
			if (err != RPC_STATUS.RPC_S_DUPLICATE_ENDPOINT)
			{
				Guard.Assert(err);
			}
		}

		private static bool serverRegisterAuthInfo(RPC_C_AUTHN auth, string serverPrincName)
		{
			RpcTrace.Verbose("serverRegisterAuthInfo({0})", auth);
			if (NativeMethods.RpcServerRegisterAuthInfo(serverPrincName, (uint)auth, IntPtr.Zero, IntPtr.Zero) != 0)
			{
				RpcTrace.Warning("serverRegisterAuthInfo - unable to register authentication type {0}", auth);
				return false;
			}
			return true;
		}

		private static void serverListen(uint maxCalls)
		{
			RpcTrace.Verbose("Begin Server Listening");
			RPC_STATUS result = NativeMethods.RpcServerListen(1u, maxCalls, 1u);
			if (result == RPC_STATUS.RPC_S_ALREADY_LISTENING)
			{
				result = RPC_STATUS.RPC_S_OK;
			}
			Guard.Assert(result);
			RpcTrace.Verbose("Server Ready");
		}

		private static void serverStopListening()
		{
			RpcTrace.Verbose("Stop Server Listening");
			RPC_STATUS result = NativeMethods.RpcMgmtStopServerListening(IntPtr.Zero);
			if (result != 0)
			{
				RpcTrace.Warning("RpcMgmtStopServerListening result = {0}", result);
			}
			result = NativeMethods.RpcMgmtWaitServerListen();
			if (result != 0)
			{
				RpcTrace.Warning("RpcMgmtWaitServerListen result = {0}", result);
			}
		}

		protected bool Equals(Server other)
		{
			return object.Equals(_handle, other._handle);
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != GetType())
			{
				return false;
			}
			return Equals((Server)obj);
		}

		public override int GetHashCode()
		{
			if (_handle == null)
			{
				return 0;
			}
			return _handle.GetHashCode();
		}
	}
}
