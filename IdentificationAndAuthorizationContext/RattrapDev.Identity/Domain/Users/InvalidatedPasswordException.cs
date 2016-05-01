using System;

namespace RattrapDev.Identity.Domain.Users
{
	public class InvalidatedPasswordException : Exception
	{
		public InvalidatedPasswordException ()
			: base("The given password did not match the password assigned to user.")
		{
		}
	}
}

