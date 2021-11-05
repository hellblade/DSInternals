using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc
{
	/// <summary>
	/// Provides a connection-based wrapper around the RPC client
	/// </summary>
	[DebuggerDisplay("{_handle} @{_binding}")]
	public class Client : IDisposable
	{
		protected class RpcClientHandle : RpcHandle
		{
			protected override void DisposeHandle(ref IntPtr handle)
			{
				if (handle != IntPtr.Zero)
				{
					Guard.Assert(NativeMethods.RpcBindingFree(ref Handle));
					handle = IntPtr.Zero;
				}
			}
		}

		protected bool _authenticated;

		private readonly RpcProtseq _protocol;

		private readonly string _binding;

		protected readonly RpcHandle _handle;

		/// <summary>
		/// The protocol that was provided to the constructor
		/// </summary>
		public RpcProtseq Protocol => _protocol;

		/// <summary>
		/// Returns a constant NetworkCredential that represents the Anonymous user
		/// </summary>
		public static NetworkCredential Anonymous => new NetworkCredential("ANONYMOUS LOGON", "", "NT_AUTHORITY");

		/// <summary>
		/// Returns a constant NetworkCredential that represents the current Windows user
		/// </summary>
		public static NetworkCredential Self => null;

		public Client(EndpointBindingInfo endpointBindingInfo)
		{
			_handle = new RpcClientHandle();
			_protocol = endpointBindingInfo.Protseq;
			_binding = stringBindingCompose(endpointBindingInfo, null);
			RpcTrace.Verbose("Client('{0}:{1}')", endpointBindingInfo.NetworkAddr, endpointBindingInfo.EndPoint);
			connect();
		}

		private static string stringBindingCompose(EndpointBindingInfo endpointBindingInfo, string Options)
		{
			IntPtr lpBindingString;
			RPC_STATUS result = NativeMethods.RpcStringBindingCompose(null, endpointBindingInfo.Protseq.ToString(), endpointBindingInfo.NetworkAddr, endpointBindingInfo.EndPoint, Options, out lpBindingString);
			Guard.Assert(result);
			try
			{
				return Marshal.PtrToStringUni(lpBindingString);
			}
			finally
			{
				Guard.Assert(NativeMethods.RpcStringFree(ref lpBindingString));
			}
		}

		/// <summary>
		/// Disconnects the client and frees any resources.
		/// </summary>
		public void Dispose()
		{
			RpcTrace.Verbose("RpcClient('{0}').Dispose()", _binding);
			_handle.Dispose();
		}

		/// <summary>
		/// Connects the client; however, this is a soft-connection and validation of 
		/// the connection will not take place until the first call is attempted.
		/// </summary>
		private void connect()
		{
			bindingFromStringBinding(_handle, _binding);
			RpcTrace.Verbose("RpcClient.connect({0} = {1})", _handle.Handle, _binding);
		}

		private static void bindingFromStringBinding(RpcHandle handle, string bindingString)
		{
			RPC_STATUS result = NativeMethods.RpcBindingFromStringBinding(bindingString, out handle.Handle);
			Guard.Assert(result);
		}

		/// <summary>
		/// Adds authentication information to the client, use the static Self to
		/// authenticate as the currently logged on Windows user.
		/// </summary>
		public void AuthenticateAs(NetworkCredential credentials)
		{
			AuthenticateAs(null, credentials);
		}

		/// <summary>
		/// Adds authentication information to the client, use the static Self to
		/// authenticate as the currently logged on Windows user.
		/// </summary>
		public void AuthenticateAs(string serverPrincipalName, NetworkCredential credentials)
		{
			RPC_C_AUTHN[] types = new RPC_C_AUTHN[2]
			{
				RPC_C_AUTHN.RPC_C_AUTHN_GSS_NEGOTIATE,
				RPC_C_AUTHN.RPC_C_AUTHN_WINNT
			};
			RPC_C_AUTHN_LEVEL protect = RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY;
			if (credentials != null && credentials.UserName == Anonymous.UserName && credentials.Domain == Anonymous.Domain)
			{
				protect = RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_DEFAULT;
				RPC_C_AUTHN[] array = new RPC_C_AUTHN[1];
				types = array;
			}
			AuthenticateAs(serverPrincipalName, credentials, protect, types);
		}

		public void AuthenticateAsNone()
		{
			_authenticated = true;
		}

		/// <summary>
		/// Adds authentication information to the client, use the static Self to
		/// authenticate as the currently logged on Windows user.  This overload allows
		/// you to specify the privacy level and authentication types to try. Normally
		/// these default to RPC_C_PROTECT_LEVEL_PKT_PRIVACY, and both RPC_C_AUTHN_GSS_NEGOTIATE
		/// or RPC_C_AUTHN_WINNT if that fails.  If credentials is null, or is the Anonymous
		/// user, RPC_C_PROTECT_LEVEL_DEFAULT and RPC_C_AUTHN_NONE are used instead.
		/// </summary>
		public void AuthenticateAs(string serverPrincipalName, NetworkCredential credentials, RPC_C_AUTHN_LEVEL level, params RPC_C_AUTHN[] authTypes)
		{
			if (!_authenticated)
			{
				bindingSetAuthInfo(level, authTypes, _handle, serverPrincipalName, credentials);
				_authenticated = true;
			}
		}

		private static void bindingSetAuthInfo(RPC_C_AUTHN_LEVEL level, RPC_C_AUTHN[] authTypes, RpcHandle handle, string serverPrincipalName, NetworkCredential credentails)
		{
			if (credentails == null)
			{
				foreach (RPC_C_AUTHN atype2 in authTypes)
				{
					RPC_STATUS result2 = NativeMethods.RpcBindingSetAuthInfo2(handle.Handle, serverPrincipalName, level, atype2, IntPtr.Zero, RPC_C_AUTHZ.RPC_C_AUTHZ_NONE);
					if (result2 != 0)
					{
						RpcTrace.Warning("Unable to register {0}, result = {1}", atype2, new RpcException(result2).Message);
					}
				}
				return;
			}
			SEC_WINNT_AUTH_IDENTITY pSecInfo = new SEC_WINNT_AUTH_IDENTITY(credentails);
			foreach (RPC_C_AUTHN atype in authTypes)
			{
				RPC_STATUS result = NativeMethods.RpcBindingSetAuthInfo(handle.Handle, serverPrincipalName, level, atype, ref pSecInfo, RPC_C_AUTHZ.RPC_C_AUTHZ_NONE);
				if (result != 0)
				{
					RpcTrace.Warning("Unable to register {0}, result = {1}", atype, new RpcException(result).Message);
				}
			}
		}

		protected bool Equals(Client other)
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
			return Equals((Client)obj);
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
