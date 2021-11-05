using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace NDceRpc.Microsoft.Interop
{
	internal class RpcCallInfo : IRpcCallInfo, IDisposable
	{
		private class IgnoreOnDispose : IDisposable
		{
			void IDisposable.Dispose()
			{
			}
		}

		private class RpcImpersonationContext : IDisposable
		{
			private readonly RpcCallInfo _call;

			public RpcImpersonationContext(RpcCallInfo call)
			{
				_call = call;
			}

			void IDisposable.Dispose()
			{
				_call.RevertToSelf();
			}
		}

		private readonly IntPtr _clientHandle;

		private WindowsIdentity _user;

		private bool _impersonating;

		private RPC_CALL_ATTRIBUTES_V2 _callAttrs;

		private byte[] _clientAddress;

		private string _clientPrincipalName;

		private bool _isAuthenticated;

		public bool IsImpersonating => _impersonating;

		public WindowsIdentity ClientUser
		{
			get
			{
				if (_user == null)
				{
					if (IsAuthenticated)
					{
						using (Impersonate())
						{
							_user = WindowsIdentity.GetCurrent(ifImpersonating: true);
						}
					}
					else
					{
						_user = WindowsIdentity.GetAnonymous();
					}
				}
				return _user;
			}
		}

		public string ClientPrincipalName
		{
			get
			{
				GetCallInfo();
				return _clientPrincipalName;
			}
		}

		public byte[] ClientAddress
		{
			get
			{
				GetCallInfo();
				if (_clientAddress != null)
				{
					return (byte[])_clientAddress.Clone();
				}
				return new byte[0];
			}
		}

		public bool IsAuthenticated
		{
			get
			{
				if (GetCallInfo().AuthenticationService == RPC_C_AUTHN.RPC_C_AUTHN_NONE)
				{
					return _isAuthenticated;
				}
				return true;
			}
		}

		public bool IsClientLocal
		{
			get
			{
				if (GetCallInfo().ProtocolSequence != RpcProtoseqType.LRPC)
				{
					return GetCallInfo().IsClientLocal == RpcCallClientLocality.Local;
				}
				return true;
			}
		}

		public RpcProtoseqType ProtocolType => GetCallInfo().ProtocolSequence;

		public RPC_C_AUTHN_LEVEL ProtectionLevel => GetCallInfo().AuthenticationLevel;

		public RPC_C_AUTHN AuthenticationLevel => GetCallInfo().AuthenticationService;

		public IntPtr ClientPid => GetCallInfo().ClientPID;

		public RpcCallInfo(IntPtr clientHandle)
		{
			_user = null;
			_impersonating = false;
			_clientHandle = clientHandle;
		}

		public IDisposable Impersonate()
		{
			if (_impersonating)
			{
				return new IgnoreOnDispose();
			}
			if (!IsAuthenticated)
			{
				throw new UnauthorizedAccessException();
			}
			Guard.Assert(NativeMethods.RpcImpersonateClient(_clientHandle));
			_impersonating = true;
			return new RpcImpersonationContext(this);
		}

		private void RevertToSelf()
		{
			if (_impersonating)
			{
				Guard.Assert(NativeMethods.RpcRevertToSelfEx(_clientHandle));
			}
			_impersonating = false;
		}

		public void Dispose()
		{
			RevertToSelf();
			if (_user != null)
			{
				_user.Dispose();
			}
			_user = null;
		}

		private RPC_CALL_ATTRIBUTES_V2 GetCallInfo()
		{
			if (_callAttrs.Version != 0)
			{
				return _callAttrs;
			}
			RPC_CALL_ATTRIBUTES_V2 rPC_CALL_ATTRIBUTES_V = default(RPC_CALL_ATTRIBUTES_V2);
			rPC_CALL_ATTRIBUTES_V.Version = 2u;
			rPC_CALL_ATTRIBUTES_V.Flags = RPC_CALL_ATTRIBUTES_FLAGS.RPC_QUERY_NO_AUTH_REQUIRED;
			RPC_CALL_ATTRIBUTES_V2 attrs = rPC_CALL_ATTRIBUTES_V;
			RPC_STATUS err = NativeMethods.RpcServerInqCallAttributes(_clientHandle, ref attrs);
			if (err == RPC_STATUS.RPC_S_INVALID_ARG)
			{
				attrs.Version = 1u;
				err = NativeMethods.RpcServerInqCallAttributes(_clientHandle, ref attrs);
			}
			if (err == RPC_STATUS.RPC_S_OK)
			{
				_callAttrs = attrs;
				_isAuthenticated = false;
				attrs.Flags = RPC_CALL_ATTRIBUTES_FLAGS.RPC_QUERY_IS_CLIENT_LOCAL | RPC_CALL_ATTRIBUTES_FLAGS.RPC_QUERY_NO_AUTH_REQUIRED;
				if ((err = NativeMethods.RpcServerInqCallAttributes(_clientHandle, ref attrs)) == RPC_STATUS.RPC_S_OK)
				{
					_callAttrs.IsClientLocal = attrs.IsClientLocal;
					if (_callAttrs.ProtocolSequence == RpcProtoseqType.LRPC)
					{
						attrs.Flags = RPC_CALL_ATTRIBUTES_FLAGS.RPC_QUERY_CLIENT_PID;
						if ((err = NativeMethods.RpcServerInqCallAttributes(_clientHandle, ref attrs)) == RPC_STATUS.RPC_S_OK)
						{
							_callAttrs.ClientPID = attrs.ClientPID;
						}
					}
				}
				if (_callAttrs.ProtocolSequence != RpcProtoseqType.LRPC)
				{
					using Ptr<byte[]> callerAddress = new Ptr<byte[]>(new byte[1024]);
					RPC_CALL_LOCAL_ADDRESS_V1 localAddress = default(RPC_CALL_LOCAL_ADDRESS_V1);
					localAddress.Version = 1u;
					localAddress.Buffer = callerAddress.Handle;
					localAddress.BufferSize = 1024;
					localAddress.AddressFormat = RpcLocalAddressFormat.Invalid;
					_callAttrs = attrs;
					using Ptr<RPC_CALL_LOCAL_ADDRESS_V1> callerAddressv1 = new Ptr<RPC_CALL_LOCAL_ADDRESS_V1>(localAddress);
					attrs.CallLocalAddress = callerAddressv1.Handle;
					attrs.Flags = RPC_CALL_ATTRIBUTES_FLAGS.RPC_QUERY_CALL_LOCAL_ADDRESS | RPC_CALL_ATTRIBUTES_FLAGS.RPC_QUERY_NO_AUTH_REQUIRED;
					if ((err = NativeMethods.RpcServerInqCallAttributes(_clientHandle, ref attrs)) == RPC_STATUS.RPC_S_OK)
					{
						_clientAddress = new byte[callerAddressv1.Data.BufferSize];
						Array.Copy(callerAddress.Data, _clientAddress, _clientAddress.Length);
					}
				}
				using Ptr<byte[]> clientPrincipal = new Ptr<byte[]>(new byte[1024]);
				attrs.ClientPrincipalName = clientPrincipal.Handle;
				attrs.ClientPrincipalNameBufferLength = 1024;
				attrs.Flags = RPC_CALL_ATTRIBUTES_FLAGS.RPC_QUERY_CLIENT_PRINCIPAL_NAME;
				if ((err = NativeMethods.RpcServerInqCallAttributes(_clientHandle, ref attrs)) == RPC_STATUS.RPC_S_OK)
				{
					_clientPrincipalName = Marshal.PtrToStringUni(clientPrincipal.Handle);
					if (!string.IsNullOrEmpty(_clientPrincipalName))
					{
						_isAuthenticated = true;
						if (attrs.Version == 1)
						{
							_callAttrs.IsClientLocal = RpcCallClientLocality.Local;
						}
					}
				}
			}
			else
			{
				RpcTrace.Warning("RpcServerInqCallAttributes error {0} = {1}", err, new RpcException(err).Message);
			}
			return _callAttrs;
		}
	}
}
