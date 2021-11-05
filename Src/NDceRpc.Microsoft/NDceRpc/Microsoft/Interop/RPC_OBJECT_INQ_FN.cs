using System;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
	/// <summary>
	/// The function is a prototype for a function that facilitates replacement of the default object UUID to type UUID mapping.
	/// </summary>
	/// <param name="ObjectUuid"></param>
	/// <param name="TypeUuid"></param>
	/// <param name="Status"></param>
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate void RPC_OBJECT_INQ_FN(ref Guid ObjectUuid, ref Guid TypeUuid, ref RPC_STATUS Status);
}
