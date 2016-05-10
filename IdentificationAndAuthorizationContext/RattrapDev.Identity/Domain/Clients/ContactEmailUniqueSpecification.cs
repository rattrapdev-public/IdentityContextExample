namespace Geonetric.Identity.Domain.Clients
{
    using System.Linq;

    using Geonetric.DDD.Domain;

    public class ContactEmailUniqueSpecification : IValidationSpecification<Client>
	{
		private readonly IClientRepository repository;

		public ContactEmailUniqueSpecification (IClientRepository repository)
		{
			this.repository = repository;
		}

		public bool IsSatisifiedBy (Client candidate)
		{
			return !(this.repository.All().Any (c => c.ContactInfo.Email.Equals(candidate.ContactInfo.Email) && !(c.Identifier.Equals(candidate.Identifier))));
		}
	}
}

