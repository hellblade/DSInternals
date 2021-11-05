using System;

namespace NDceRpc.Native
{
	public class NativeClient : Client
	{
		public IntPtr Binding => _handle.Handle;

		public NativeClient(EndpointBindingInfo info)
			: base(info)
		{
		}
	}
}
