using System;
using System.Linq;
using RattrapDev.DDD;

namespace RattrapDev.Identity.Domain.Client
{
	public class ContactEmailUniqueSpecification : IValidationSpecification<Client>
	{
		private readonly IClientRepository repository;

		public ContactEmailUniqueSpecification (IClientRepository repository)
		{
			this.repository = repository;
		}

		public bool IsSatisifiedBy (Client candidate)
		{
			return !(repository.All().Any (c => c.ContactInfo.Email.Equals(candidate.ContactInfo.Email) && !(c.Identifier.Equals(candidate.Identifier))));
		}
	}
}

