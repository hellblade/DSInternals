using System;

namespace NDceRpc.Microsoft.Interop
{
	public struct RPC_MESSAGE
	{
		private IntPtr Handle;

		private uint DataRepresentation;

		private IntPtr Buffer;

		private ushort BufferLength;

		private ushort ProcNum;

		private IntPtr TransferSyntax;

		private IntPtr RpcInterfaceInformation;

		private IntPtr ReservedForRuntime;

		private IntPtr ManagerEpv;

		private IntPtr ImportContext;

		private uint RpcFlags;
	}
}
