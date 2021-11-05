using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
	/// <summary>
	/// Handle to the structure containing the client's authentication and authorization credentials appropriate for the selected authentication and authorization service.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	[DebuggerDisplay("{Domain}\\{User}")]
	public struct SEC_WINNT_AUTH_IDENTITY
	{
		private const uint SEC_WINNT_AUTH_IDENTITY_UNICODE = 2u;

		private readonly string User;

		private readonly uint UserLength;

		private readonly string Domain;

		private readonly uint DomainLength;

		private readonly string Password;

		private readonly uint PasswordLength;

		private readonly uint Flags;

		public SEC_WINNT_AUTH_IDENTITY(NetworkCredential cred)
			: this(cred.Domain, cred.UserName, cred.Password)
		{
		}

		public SEC_WINNT_AUTH_IDENTITY(string domain, string user, string password)
		{
			User = user;
			UserLength = (uint)user.Length;
			Domain = domain;
			DomainLength = (uint)domain.Length;
			Password = password;
			PasswordLength = (uint)password.Length;
			Flags = 2u;
		}
	}
}
