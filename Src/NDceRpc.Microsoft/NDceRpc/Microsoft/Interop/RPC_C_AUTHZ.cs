namespace NDceRpc.Microsoft.Interop
{
	/// <summary>
	/// The authorization service constants represent the authorization services passed to various run-time functions.
	/// </summary>
	public enum RPC_C_AUTHZ : uint
	{
		/// <summary>
		/// Server performs no authorization.
		/// </summary>
		RPC_C_AUTHZ_NONE = 0u,
		RPC_C_AUTHZ_NAME = 1u,
		RPC_C_AUTHZ_DCE = 2u,
		RPC_C_AUTHZ_DEFAULT = uint.MaxValue
	}
}
