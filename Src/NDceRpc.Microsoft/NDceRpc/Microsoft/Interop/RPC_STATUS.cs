namespace NDceRpc.Microsoft.Interop
{
	/// <summary> WIN32 RPC Error Codes </summary>
	/// <seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms681386.aspx" />
	public enum RPC_STATUS : uint
	{
		RPC_S_OK = 0u,
		RPC_S_INVALID_ARG = 87u,
		RPC_S_OUT_OF_MEMORY = 14u,
		RPC_S_OUT_OF_THREADS = 164u,
		RPC_S_INVALID_LEVEL = 87u,
		RPC_S_BUFFER_TOO_SMALL = 122u,
		RPC_S_INVALID_SECURITY_DESC = 1338u,
		RPC_S_ACCESS_DENIED = 5u,
		RPC_S_SERVER_OUT_OF_MEMORY = 1130u,
		RPC_S_ASYNC_CALL_PENDING = 997u,
		RPC_S_UNKNOWN_PRINCIPAL = 1332u,
		RPC_S_TIMEOUT = 1460u,
		/// <summary>
		/// The binding handle is not the correct type.
		/// </summary>
		RPC_S_WRONG_KIND_OF_BINDING = 1701u,
		/// <summary>
		/// The binding handle is invalid.
		/// </summary>
		RPC_S_INVALID_BINDING = 1702u,
		/// <summary>
		/// The object universal unique identifier (UUID) has already been registered.
		/// </summary>
		RPC_S_ALREADY_REGISTERED = 1711u,
		RPC_S_TYPE_ALREADY_REGISTERED = 1712u,
		RPC_S_ALREADY_LISTENING = 1713u,
		RPC_S_NO_PROTSEQS_REGISTERED = 1714u,
		RPC_S_NOT_LISTENING = 1715u,
		RPC_S_DUPLICATE_ENDPOINT = 1740u,
		RPC_S_BINDING_HAS_NO_AUTH = 1746u,
		RPC_S_CANNOT_SUPPORT = 1764u,
		/// <summary>
		/// The string is too long.
		/// </summary>
		RPC_S_STRING_TOO_LONG = 1743u,
		/// <summary>
		/// The object universal unique identifier (UUID) is the nil UUID.
		/// </summary>
		RPC_S_INVALID_OBJECT = 1900u,
		RPC_E_FAIL = 2147500037u
	}
}
