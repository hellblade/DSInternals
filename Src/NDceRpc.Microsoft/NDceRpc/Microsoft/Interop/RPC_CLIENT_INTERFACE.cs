using System;

namespace NDceRpc.Microsoft.Interop
{
	public struct RPC_CLIENT_INTERFACE
	{
		public uint Length;

		public RPC_SYNTAX_IDENTIFIER InterfaceId;

		public RPC_SYNTAX_IDENTIFIER TransferSyntax;

		public IntPtr DispatchTable;

		public uint RpcProtseqEndpointCount;

		public IntPtr RpcProtseqEndpoint;

		public IntPtr Reserved;

		public IntPtr InterpreterInfo;

		public uint Flags;
	}
}
