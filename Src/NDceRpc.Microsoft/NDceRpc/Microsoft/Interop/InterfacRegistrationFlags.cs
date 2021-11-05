using System;

namespace NDceRpc.Microsoft.Interop
{
	/// <summary>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/aa373954.aspx
	/// </summary>
	[Flags]
	public enum InterfacRegistrationFlags : uint
	{
		/// <summary>
		///
		/// </summary>
		Standard_interface_semantics = 0x0u,
		RPC_IF_AUTOLISTEN = 0x1u,
		RPC_IF_OLE = 0x2u,
		RPC_IF_ALLOW_UNKNOWN_AUTHORITY = 0x4u,
		RPC_IF_ALLOW_SECURE_ONLY = 0x8u,
		/// <summary>
		/// When this interface flag is registered, the RPC runtime invokes the registered security callback for all calls, regardless of identity, protocol sequence, or authentication level of the client. This flag is allowed only when a security callback is registered.
		/// Note  This flag is available starting with Windows XP with SP2 and Windows Server 2003 with SP1. When this flag is not set, RPC automatically filters all unauthenticated calls before they reach the security callback.
		/// </summary>
		RPC_IF_ALLOW_CALLBACKS_WITH_NO_AUTH = 0x10u,
		RPC_IF_ALLOW_LOCAL_ONLY = 0x20u,
		RPC_IF_SEC_NO_CACHE = 0x40u,
		RPC_IF_SEC_CACHE_PER_PROC = 0x80u,
		RPC_IF_ASYNC_CALLBACK = 0x100u
	}
}
