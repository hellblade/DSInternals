using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc
{
	/// <summary>
	/// Exception class: RpcException : System.ComponentModel.Win32Exception
	/// Unspecified rpc error
	/// </summary>
	[Serializable]
	public class RpcException : Win32Exception
	{
		/// <summary>
		/// Returns the RPC Error as an enumeration
		/// </summary>
		public RPC_STATUS RpcStatus => (RPC_STATUS)base.NativeErrorCode;

		/// <summary>
		/// Serialization constructor
		/// </summary>
		protected RpcException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>
		/// Unspecified rpc error
		/// </summary>
		public RpcException()
			: base(ErrorMessages.RpcDefaultError)
		{
		}

		/// <summary>
		/// Unspecified rpc error
		/// </summary>
		public RpcException(Exception innerException)
			: base(ErrorMessages.RpcDefaultError, innerException)
		{
		}

		/// <summary>
		/// if(condition == false) throws Unspecified rpc error
		/// </summary>
		public static void Assert(bool condition)
		{
			if (!condition)
			{
				throw new RpcException();
			}
		}

		/// <summary>
		/// </summary>
		public RpcException(string message)
			: base(message)
		{
		}

		/// <summary>
		///
		/// </summary>
		public RpcException(string message, Exception innerException)
			: base(string.Format(ErrorMessages.RpcDefaultError, message), innerException)
		{
		}

		/// <summary>
		/// Exception class: RpcException : System.ComponentModel.Win32Exception
		/// Unspecified rpc error
		/// </summary>
		public RpcException(RPC_STATUS errorsCode)
			: base((int)errorsCode)
		{
		}
	}
}
