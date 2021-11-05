using System;

namespace NDceRpc.Microsoft.Interop
{
	public struct MIDL_SERVER_INFO
	{
		public IntPtr pStubDesc;

		public IntPtr DispatchTable;

		public IntPtr ProcString;

		public IntPtr FmtStringOffset;

		public IntPtr ThunkTable;

		public IntPtr pTransferSyntax;

		public IntPtr nCount;

		public IntPtr pSyntaxInfo;
	}
}
