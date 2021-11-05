using System;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
	[StructLayout(LayoutKind.Explicit)]
	public struct CLIENT_CALL_RETURN
	{
		[FieldOffset(0)]
		private IntPtr Pointer;

		[FieldOffset(0)]
		private int Simple;
	}
}
