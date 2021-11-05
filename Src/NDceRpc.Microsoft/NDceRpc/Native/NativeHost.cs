using System;
using System.Collections.Generic;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc.Native
{
	public static class NativeHost
	{
		public static List<object> _pinned = new List<object>();

		public static NativeServer StartServer(EndpointBindingInfo info, IntPtr dummyPtr)
		{
			return StartServer(info, dummyPtr, IntPtr.Zero, IntPtr.Zero);
		}

		public static NativeServer StartServer(EndpointBindingInfo info, IntPtr dummyPtr, IntPtr mgrTypeUuid, IntPtr mgrEpv)
		{
			NativeServer server = new NativeServer(dummyPtr, mgrTypeUuid, mgrEpv);
			server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_NONE);
			server.AddProtocol(info.Protseq, info.EndPoint, 255u);
			server.StartListening();
			return server;
		}
	}
}
