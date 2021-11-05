using System;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate void RPC_DISPATCH_FUNCTION(ref IntPtr Message);
}
