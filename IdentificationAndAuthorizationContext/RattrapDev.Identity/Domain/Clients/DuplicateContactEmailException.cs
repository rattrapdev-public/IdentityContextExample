using System;

namespace RattrapDev.Identity.Domain.Clients
{
	public class DuplicateContactEmailException : Exception
	{
		public DuplicateContactEmailException ()
			: base("The contact email is already assigned to another client!")
		{
		}
	}
}

