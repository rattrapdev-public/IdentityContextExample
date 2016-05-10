namespace Geonetric.Identity.Domain.Clients
{
    using System;

    public class DuplicateClientException : Exception
	{
		public DuplicateClientException ()
			: base("The Client Name is already assigned to another client!")
		{
		}
	}
}

