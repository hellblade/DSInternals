namespace NDceRpc.Microsoft.Interop
{
	/// <summary>
	/// Defines the type of protocol the client is connected with
	/// </summary>
	public enum RpcProtoseqType : uint
	{
		/// <summary> TCP, UDP, IPX over TCP, etc </summary>
		TCP = 1u,
		/// <summary> Named Pipes </summary>
		NMP,
		/// <summary> LPRC / Local RPC </summary>
		LRPC,
		/// <summary> HTTP / IIS integrated </summary>
		HTTP
	}
}
