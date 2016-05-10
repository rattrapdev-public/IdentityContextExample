namespace Geonetric.Identity.Domain.Clients
{
    using System;

    public class DuplicateContactEmailException : Exception
	{
		public DuplicateContactEmailException ()
			: base("The contact email is already assigned to another client!")
		{
		}
	}
}

