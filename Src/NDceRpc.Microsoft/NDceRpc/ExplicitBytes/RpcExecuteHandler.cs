namespace NDceRpc.ExplicitBytes
{
	/// <summary>
	/// The delegate format for the OnExecute event
	/// </summary>
	public delegate byte[] RpcExecuteHandler(IRpcCallInfo call, byte[] input);
}
