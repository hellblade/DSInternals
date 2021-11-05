using System;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
	/// Return Type: RPC_STATUS-&gt;int  
	///             InterfaceUuid: RPC_IF_HANDLE-&gt;void*  
	///             Context: void*  
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate RPC_STATUS RPC_IF_CALLBACK_FN(IntPtr Interface, IntPtr Context);
}
