using System;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
	public class Ptr<T> : IDisposable
	{
		private readonly GCHandle _handle;

		public T Data => (T)_handle.Target;

		public IntPtr Handle => _handle.AddrOfPinnedObject();

		public Ptr(T data)
		{
			_handle = GCHandle.Alloc(data, GCHandleType.Pinned);
		}

		public void Dispose()
		{
			_handle.Free();
		}
	}
}
