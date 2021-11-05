using System;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
	public struct RPC_SERVER_INTERFACE
	{
		public uint Length;

		public RPC_SYNTAX_IDENTIFIER InterfaceId;

		public RPC_SYNTAX_IDENTIFIER TransferSyntax;

		public IntPtr DispatchTable;

		public uint RpcProtseqEndpointCount;

		public IntPtr RpcProtseqEndpoint;

		public IntPtr DefaultManagerEpv;

		public IntPtr InterpreterInfo;

		public uint Flags;

		public RPC_DISPATCH_TABLE? DispatchTableValue
		{
			get
			{
				if (DispatchTable == IntPtr.Zero)
				{
					return null;
				}
				return (RPC_DISPATCH_TABLE)Marshal.PtrToStructure(DispatchTable, typeof(RPC_DISPATCH_TABLE));
			}
		}
	}
}
