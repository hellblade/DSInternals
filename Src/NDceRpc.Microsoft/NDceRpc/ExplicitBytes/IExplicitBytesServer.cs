namespace NDceRpc.ExplicitBytes
{
	/// <summary>
	/// Server side transport interface.
	/// </summary>
	public interface IExplicitBytesServer
	{
		/// <summary>
		///
		/// </summary>
		event RpcExecuteHandler OnExecute;

		/// <summary>
		///
		/// </summary>
		void StartListening();

		/// <summary>
		///
		/// </summary>
		void Dispose();
	}
}
