using System;

namespace NDceRpc.Microsoft.Interop.Async
{
	/// <summary>
	/// The structure holds the state of an asynchronous remote procedure call. Used to wait for, query, reply to, or cancel asynchronous calls.
	/// </summary>
	/// <seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa378490.aspx" />
	public struct RPC_ASYNC_STATE
	{
		/// <summary>
		/// Size of this structure, in bytes. The environment sets this member when <see cref="M:NDceRpc.Microsoft.Interop.NativeMethods.RpcAsyncInitializeHandle(NDceRpc.Microsoft.Interop.Async.RPC_ASYNC_STATE@,System.UInt16)" /> is called. Do not modify this member.
		/// </summary>
		private ushort Size;

		/// <summary>
		/// The run-time environment sets this member when <see cref="!:NDceRpc.NativeMethodscInitializeHandle" /> is called. Do not modify this member.
		/// </summary>
		private uint Signature;

		/// <summary>
		/// The run-time environment sets this member when <see cref="!:NDceRpc.NativeMethodscInitializeHandle" /> is called. Do not modify this member.
		/// </summary>
		private int Lock;

		/// <summary>
		/// <seealso cref="F:NDceRpc.Microsoft.Interop.RpcAsync.RPC_C_NOTIFY_ON_SEND_COMPLETE" />
		/// </summary>
		private uint Flags;

		/// <summary>
		/// Reserved for use by the stubs. Do not use this member.
		/// </summary>
		private IntPtr StubInfo;

		/// <summary>
		/// Use this member for any application-specific information that you want to keep track of in this structure.
		/// </summary>
		private IntPtr UserInfo;

		/// <summary>
		/// Reserved for use by the RPC run-time environment. Do not use this member.
		/// </summary>
		private IntPtr RuntimeInfo;

		private RPC_ASYNC_EVENT Event;

		private RPC_NOTIFICATION_TYPES NotificationType;

		private RPC_ASYNC_NOTIFICATION_INFO u;

		/// <summary>
		/// Reserved for compatibility with future versions, if any. Do not use this member.
		/// </summary>
		private IntPtr[] Reserved;
	}
}
