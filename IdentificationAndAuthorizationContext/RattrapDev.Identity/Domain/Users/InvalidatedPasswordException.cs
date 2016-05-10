namespace Geonetric.Identity.Domain.Users
{
    using System;

    public class InvalidatedPasswordException : Exception
	{
		public InvalidatedPasswordException ()
			: base("The given password did not match the password assigned to user.")
		{
		}
	}
}

