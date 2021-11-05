using System;
using System.Runtime.InteropServices;
using System.Text;
using NDceRpc.Microsoft.Interop.Async;

namespace NDceRpc.Microsoft.Interop
{
	/// <summary>
	///
	/// </summary>
	public static class NativeMethods
	{
		/// <seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa375771.aspx" />
		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcMgmtSetCancelTimeout(int Seconds);

		/// <seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa375746.aspx" />
		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcMgmtInqComTimeout(IntPtr Binding, out uint Timeout);

		/// <summary>
		///  This option is ignored for <seealso cref="F:NDceRpc.Microsoft.Interop.RpcProtseq.ncalrpc" />
		///  </summary>
		/// <seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa375779.aspx" />
		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcMgmtSetComTimeout(IntPtr Binding, uint Timeout);

		/// <summary>
		/// The function registers an object-inquiry function. A null value turns off a previously registered object-inquiry function.
		/// </summary>
		/// <param name="InquiryFn"></param>
		/// <returns></returns>
		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcObjectSetInqFn(RPC_OBJECT_INQ_FN InquiryFn);

		/// <summary>
		///  The function sets the object UUID value in a binding handle.
		///  </summary>
		///  <param name="Binding">Server binding into which the ObjectUuid is set.</param>
		///  <param name="ObjectUuid">
		///  Pointer to the UUID of the object serviced by the server specified in the Binding parameter. 
		///  ObjectUuid is a unique identifier of an object to which a remote procedure call can be made.
		///  </param>
		///  <seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa375609.aspx" />
		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcBindingSetObject(IntPtr Binding, ref Guid ObjectUuid);

		/// <summary>
		/// The RpcObjectSetType function assigns the type of an object.
		/// </summary>
		/// <param name="ObjUuid">Pointer to an object UUID to associate with the type UUID in the TypeUuid parameter.</param>
		/// <param name="TypeUuid">
		/// Pointer to the type UUID of the ObjUuid parameter. 
		/// Specify a parameter value of NULL or a nil UUID to reset the object type to the default association of object UUID/nil-type UUID.
		/// </param>
		/// <returns></returns>
		/// <seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa378427.aspx" />
		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcObjectSetType(ref Guid ObjUuid, ref Guid TypeUuid);

		/// <summary>
		/// The RpcBindingReset function resets a binding handle so that the host is specified but the server on that host is unspecified.
		/// </summary>
		/// <param name="Binding">Server binding handle to reset.</param>
		/// <returns>
		///        <see cref="F:NDceRpc.Microsoft.Interop.RPC_STATUS.RPC_S_OK" /> The call succeeded.
		///             <see cref="F:NDceRpc.Microsoft.Interop.RPC_STATUS.RPC_S_INVALID_BINDING" />  The binding handle was invalid.
		///             <see cref="F:NDceRpc.Microsoft.Interop.RPC_STATUS.RPC_S_WRONG_KIND_OF_BINDING" /> This was the wrong kind of binding for the operation.
		/// </returns>
		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcBindingReset(IntPtr Binding);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcAsyncInitializeHandle(ref RPC_ASYNC_STATE pAsync, ushort Size);

		/// <summary>
		///  Validates the format of the string binding handle and converts
		///  it to a binding handle.
		///  Connection is not done here either.
		///  </summary>
		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "RpcBindingFromStringBindingW", SetLastError = true)]
		public static extern RPC_STATUS RpcBindingFromStringBinding(string bindingString, out IntPtr lpBinding);

		/// <summary>
		/// The function sets a binding handle's authentication and authorization information.
		/// </summary>
		/// <param name="Binding"></param>
		/// <param name="ServerPrincName"></param>
		/// <param name="AuthnLevel"></param>
		/// <param name="AuthnSvc">Authentication service to use. </param>
		/// <param name="AuthIdentity"></param>
		/// <param name="AuthzService">Authorization service implemented by the server for the interface of interest. </param>
		/// <returns></returns>
		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "RpcBindingSetAuthInfoW", SetLastError = true)]
		public static extern RPC_STATUS RpcBindingSetAuthInfo(IntPtr Binding, string ServerPrincName, RPC_C_AUTHN_LEVEL AuthnLevel, RPC_C_AUTHN AuthnSvc, [In] ref SEC_WINNT_AUTH_IDENTITY AuthIdentity, RPC_C_AUTHZ AuthzService);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "RpcBindingSetAuthInfoW", SetLastError = true)]
		public static extern RPC_STATUS RpcBindingSetAuthInfo2(IntPtr Binding, string ServerPrincName, RPC_C_AUTHN_LEVEL AuthnLevel, RPC_C_AUTHN AuthnSvc, IntPtr p, RPC_C_AUTHZ AuthzService);

		/// <summary>
		///
		/// </summary>
		/// <param name="ClientBinding"></param>
		/// <param name="Privs">Returns a pointer to a handle to the privileged information for the client application that made the remote procedure call on the ClientBinding binding handle. For ncalrpc calls, Privs contains a string with the client's principal name.</param>
		/// <param name="ServerPrincName"></param>
		/// <param name="AuthnLevel"></param>
		/// <param name="AuthnSvc"></param>
		/// <param name="AuthzSvc"></param>
		/// <returns></returns>
		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "RpcBindingInqAuthClientW", SetLastError = true)]
		public static extern RPC_STATUS RpcBindingInqAuthClient(IntPtr ClientBinding, ref IntPtr Privs, StringBuilder ServerPrincName, ref RPC_C_AUTHN_LEVEL AuthnLevel, ref RPC_C_AUTHN AuthnSvc, ref RPC_C_AUTHZ AuthzSvc);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "RpcBindingInqAuthInfoW", SetLastError = true)]
		public static extern RPC_STATUS RpcBindingInqAuthInfo(IntPtr Binding, StringBuilder ServerPrincName, ref RPC_C_AUTHN_LEVEL AuthnLevel, ref RPC_C_AUTHN AuthnSvc, ref IntPtr AuthIdentity, ref RPC_C_AUTHZ AuthzSvc);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "NdrClientCall2", SetLastError = true)]
		public static extern IntPtr NdrClientCall2x86(IntPtr pMIDL_STUB_DESC, IntPtr formatString, IntPtr args);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "NdrClientCall2", SetLastError = true)]
		public static extern IntPtr NdrClientCall2x64(IntPtr pMIDL_STUB_DESC, IntPtr formatString, IntPtr Handle, int DataSize, IntPtr Data, out int ResponseSize, out IntPtr Response);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "RpcStringFreeW", SetLastError = true)]
		public static extern RPC_STATUS RpcStringFree(ref IntPtr lpString);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcBindingFree(ref IntPtr lpString);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "RpcStringBindingComposeW", SetLastError = true)]
		public static extern RPC_STATUS RpcStringBindingCompose(string ObjUuid, string ProtSeq, string NetworkAddr, string Endpoint, string Options, out IntPtr lpBindingString);

		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr LocalFree(IntPtr memHandle);

		/// Return Type: RPC_STATUS-&gt;int  
		///             IfSpec: RPC_IF_HANDLE-&gt;void*  
		///             MgrTypeUuid: UUID*  
		///             MgrEpv: void*  
		///             Flags: unsigned int  
		///             MaxCalls: unsigned int  
		///             IfCallback: RPC_IF_CALLBACK_FN*  
		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int RpcServerRegisterIfEx(IntPtr IfSpec, IntPtr MgrTypeUuid, IntPtr MgrEpv, InterfacRegistrationFlags Flags, uint MaxCalls, ref RPC_IF_CALLBACK_FN IfCallback);

		/// <summary>
		///
		/// </summary>
		/// <param name="IfSpec"></param>
		/// <param name="MgrTypeUuid"></param>
		/// <param name="MgrEpv"></param>
		/// <returns></returns>
		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcServerRegisterIf(IntPtr IfSpec, IntPtr MgrTypeUuid, IntPtr MgrEpv);

		/// IfSpec: RPC_IF_HANDLE-&gt;void*     
		/// MgrTypeUuid: UUID*     
		/// MgrEpv: void*     
		/// Flags: unsigned int     
		/// MaxCalls: unsigned int     
		/// MaxRpcSize: unsigned int     
		/// IfCallbackFn: RPC_IF_CALLBACK_FN*     
		[DllImport("rpcrt4.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern RPC_STATUS RpcServerRegisterIf2(IntPtr IfSpec, ref Guid MgrTypeUuid, IntPtr MgrEpv, uint Flags, uint MaxCalls, uint MaxRpcSize, ref RPC_IF_CALLBACK_FN IfCallbackFn);

		/// <summary>
		///  The  function returns the message text for a status code.
		///  </summary>
		///  <param name="ErrorText">Returns the text corresponding to the error code.</param>
		///  <param name="StatusToConvert">Status code to convert to a text string.</param>
		///  <seealso href="http://msdn.microsoft.com/en-us/library/aa373623.aspx" />
		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS DceErrorInqText(uint StatusToConvert, out string ErrorText);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcServerUnregisterIf(IntPtr IfSpec, IntPtr MgrTypeUuid, uint WaitForCallsToComplete);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "RpcServerUseProtseqEpW", SetLastError = true)]
		public static extern RPC_STATUS RpcServerUseProtseqEp(string Protseq, uint MaxCalls, string Endpoint, IntPtr SecurityDescriptor);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern void NdrServerCall2(IntPtr ptr);

		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr LocalAlloc(uint flags, uint nBytes);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "RpcServerInqCallAttributesW", SetLastError = true)]
		public static extern RPC_STATUS RpcServerInqCallAttributes(IntPtr binding, [In][Out] ref RPC_CALL_ATTRIBUTES_V2 attributes);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcImpersonateClient(IntPtr binding);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcRevertToSelfEx(IntPtr binding);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcServerListen(uint MinimumCallThreads, uint MaxCalls, uint DontWait);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcMgmtStopServerListening(IntPtr ignore);

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcMgmtWaitServerListen();

		[DllImport("Rpcrt4.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "RpcServerRegisterAuthInfoW", SetLastError = true)]
		public static extern RPC_STATUS RpcServerRegisterAuthInfo(string ServerPrincName, uint AuthnSvc, IntPtr GetKeyFn, IntPtr Arg);
	}
}
