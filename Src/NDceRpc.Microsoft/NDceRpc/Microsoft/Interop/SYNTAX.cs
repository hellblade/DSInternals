using System;

namespace NDceRpc.Microsoft.Interop
{
	internal class SYNTAX
	{
		public static readonly RPC_VERSION SYNTAX_VERSION = new RPC_VERSION
		{
			MajorVersion = 2,
			MinorVersion = 0
		};

		public static readonly Guid SYNTAX_IID = new Guid(2324192516u, 7403, 4553, 159, 232, 8, 0, 43, 16, 72, 96);
	}
}
