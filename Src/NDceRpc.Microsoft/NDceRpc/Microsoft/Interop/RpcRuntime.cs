using System;

namespace NDceRpc.Microsoft.Interop
{
	public static class RpcRuntime
	{
		internal delegate void ServerEntryPoint(IntPtr ptr);

		private const uint LPTR = 64u;

		internal static readonly bool Is64BitProcess;

		public static readonly byte[] TYPE_FORMAT;

		public static readonly byte[] FUNC_FORMAT;

		internal static readonly Ptr<byte[]> FUNC_FORMAT_PTR;

		internal static FunctionPtr<ServerEntryPoint> ServerEntry;

		internal static FunctionPtr<LocalAlloc> AllocPtr;

		internal static FunctionPtr<LocalFree> FreePtr;

		static RpcRuntime()
		{
			ServerEntry = new FunctionPtr<ServerEntryPoint>(NativeMethods.NdrServerCall2);
			AllocPtr = new FunctionPtr<LocalAlloc>(Alloc);
			FreePtr = new FunctionPtr<LocalFree>(Free);
			Is64BitProcess = IntPtr.Size == 8;
			RpcTrace.Verbose("Is64BitProcess = {0}", Is64BitProcess);
			if (Is64BitProcess)
			{
				TYPE_FORMAT = new byte[39]
				{
					0, 0, 27, 0, 1, 0, 40, 0, 8, 0,
					1, 0, 2, 91, 17, 12, 8, 92, 17, 20,
					2, 0, 18, 0, 2, 0, 27, 0, 1, 0,
					40, 84, 24, 0, 1, 0, 2, 91, 0
				};
				FUNC_FORMAT = new byte[61]
				{
					0, 72, 0, 0, 0, 0, 0, 0, 48, 0,
					50, 0, 0, 0, 8, 0, 36, 0, 71, 5,
					10, 7, 1, 0, 1, 0, 0, 0, 0, 0,
					72, 0, 8, 0, 8, 0, 11, 0, 16, 0,
					2, 0, 80, 33, 24, 0, 8, 0, 19, 32,
					32, 0, 18, 0, 112, 0, 40, 0, 16, 0,
					0
				};
			}
			else
			{
				TYPE_FORMAT = new byte[39]
				{
					0, 0, 27, 0, 1, 0, 40, 0, 4, 0,
					1, 0, 2, 91, 17, 12, 8, 92, 17, 20,
					2, 0, 18, 0, 2, 0, 27, 0, 1, 0,
					40, 84, 12, 0, 1, 0, 2, 91, 0
				};
				FUNC_FORMAT = new byte[59]
				{
					0, 72, 0, 0, 0, 0, 0, 0, 24, 0,
					50, 0, 0, 0, 8, 0, 36, 0, 71, 5,
					8, 7, 1, 0, 1, 0, 0, 0, 72, 0,
					4, 0, 8, 0, 11, 0, 8, 0, 2, 0,
					80, 33, 12, 0, 8, 0, 19, 32, 16, 0,
					18, 0, 112, 0, 20, 0, 16, 0, 0
				};
			}
			FUNC_FORMAT_PTR = new Ptr<byte[]>(FUNC_FORMAT);
		}

		internal static void Free(IntPtr ptr)
		{
			if (ptr != IntPtr.Zero)
			{
				RpcTrace.Verbose("LocalFree({0})", ptr);
				NativeMethods.LocalFree(ptr);
			}
		}

		internal static IntPtr Alloc(uint size)
		{
			IntPtr ptr = NativeMethods.LocalAlloc(64u, size);
			RpcTrace.Verbose("{0} = LocalAlloc({1})", ptr, size);
			return ptr;
		}
	}
}
