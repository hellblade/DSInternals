using System;
using System.Diagnostics;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc
{
	/// <seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa378481.aspx" />
	[DebuggerDisplay("{Protseq} {NetworkAddr} {EndPoint}")]
	public struct EndpointBindingInfo : ICloneable
	{
		public RpcProtseq Protseq;

		public string NetworkAddr;

		public string EndPoint;

		public EndpointBindingInfo(RpcProtseq protseq, string networkAddr, string endPoint)
		{
			Protseq = protseq;
			NetworkAddr = networkAddr;
			EndPoint = endPoint;
		}

		public object Clone()
		{
			return new EndpointBindingInfo(Protseq, NetworkAddr, EndPoint);
		}
	}
}
