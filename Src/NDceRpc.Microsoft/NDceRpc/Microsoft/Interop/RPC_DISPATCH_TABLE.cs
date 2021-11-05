using System;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
	public struct RPC_DISPATCH_TABLE
	{
		public uint DispatchTableCount;

		/// <summary>
		/// Pointer to first <see cref="T:NDceRpc.Microsoft.Interop.RPC_DISPATCH_FUNCTION" />
		/// </summary>
		public IntPtr DispatchTable;

		public IntPtr Reserved;

		public RPC_DISPATCH_FUNCTION FirstDispatchFunction => (RPC_DISPATCH_FUNCTION)Marshal.GetDelegateForFunctionPointer(DispatchTable, typeof(RPC_DISPATCH_FUNCTION));
	}
}
