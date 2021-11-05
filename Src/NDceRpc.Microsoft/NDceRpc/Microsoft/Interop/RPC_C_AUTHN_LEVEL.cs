namespace NDceRpc.Microsoft.Interop
{
	/// <summary>
	/// The authentication-level constants represent authentication levels passed to various run-time functions. These levels are listed in order of increasing authentication. Each new level adds to the authentication provided by the previous level. If the RPC run-time library does not support the specified level, it automatically upgrades to the next higher supported level.
	/// The protection level of the communications, <see cref="F:NDceRpc.Microsoft.Interop.RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY" /> is the default for authenticated communications.
	/// </summary>
	/// <remarks>
	/// Regardless of the value specified by the constant, ncalrpc always uses <see cref="F:NDceRpc.Microsoft.Interop.RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY" />.
	/// </remarks>
	public enum RPC_C_AUTHN_LEVEL : uint
	{
		RPC_C_AUTHN_LEVEL_DEFAULT,
		/// <summary>
		/// Performs no authentication
		/// </summary>
		RPC_C_AUTHN_LEVEL_NONE,
		RPC_C_AUTHN_LEVEL_CONNECT,
		RPC_C_AUTHN_LEVEL_CALL,
		RPC_C_AUTHN_LEVEL_PKT,
		RPC_C_AUTHN_LEVEL_PKT_INTEGRITY,
		RPC_C_AUTHN_LEVEL_PKT_PRIVACY
	}
}
