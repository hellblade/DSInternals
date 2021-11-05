using System;

namespace NDceRpc.Microsoft.Interop
{
	internal struct MIDL_STUB_DESC
	{
		public IntPtr RpcInterfaceInformation;

		public IntPtr pfnAllocate;

		public IntPtr pfnFree;

		public IntPtr pAutoBindHandle;

		private IntPtr apfnNdrRundownRoutines;

		private IntPtr aGenericBindingRoutinePairs;

		private IntPtr apfnExprEval;

		private IntPtr aXmitQuintuple;

		public IntPtr pFormatTypes;

		public int fCheckBounds;

		public uint Version;

		private IntPtr pMallocFreeStruct;

		public int MIDLVersion;

		public IntPtr CommFaultOffsets;

		private IntPtr aUserMarshalQuadruple;

		private IntPtr NotifyRoutineTable;

		public IntPtr mFlags;

		private IntPtr CsRoutineTables;

		private IntPtr ProxyServerInfo;

		private IntPtr pExprInfo;

		public MIDL_STUB_DESC(RpcHandle handle, IntPtr interfaceInfo, byte[] formatTypes, bool serverSide)
		{
			RpcInterfaceInformation = interfaceInfo;
			pfnAllocate = RpcRuntime.AllocPtr.Handle;
			pfnFree = RpcRuntime.FreePtr.Handle;
			pAutoBindHandle = (serverSide ? IntPtr.Zero : handle.Pin(default(IntPtr)));
			apfnNdrRundownRoutines = default(IntPtr);
			aGenericBindingRoutinePairs = default(IntPtr);
			apfnExprEval = default(IntPtr);
			aXmitQuintuple = default(IntPtr);
			pFormatTypes = handle.Pin(formatTypes);
			fCheckBounds = 1;
			Version = 327682u;
			pMallocFreeStruct = default(IntPtr);
			MIDLVersion = 100663657;
			CommFaultOffsets = (serverSide ? IntPtr.Zero : handle.Pin(new COMM_FAULT_OFFSETS
			{
				CommOffset = -1,
				FaultOffset = -1
			}));
			aUserMarshalQuadruple = default(IntPtr);
			NotifyRoutineTable = default(IntPtr);
			mFlags = new IntPtr(1);
			CsRoutineTables = default(IntPtr);
			ProxyServerInfo = default(IntPtr);
			pExprInfo = default(IntPtr);
		}
	}
}
