using System;

namespace RattrapDev.Identity.Domain.Clients
{
	public class DuplicateClientException : Exception
	{
		public DuplicateClientException ()
			: base("The Client Name is already assigned to another client!")
		{
		}
	}
}

