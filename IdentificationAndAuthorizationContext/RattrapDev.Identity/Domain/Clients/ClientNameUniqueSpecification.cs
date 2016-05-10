namespace Geonetric.Identity.Domain.Clients
{
    using System.Linq;

    using Geonetric.DDD.Domain;

    public class ClientNameUniqueSpecification : IValidationSpecification<Client>
	{
		private IClientRepository repository;

		public ClientNameUniqueSpecification (IClientRepository repository)
		{
			this.repository = repository;
		}

		#region IValidationSpecification implementation

		public bool IsSatisifiedBy (Client candidate)
		{
			return !(this.repository.All().Any (c => c.ClientDetails.Name.Equals(candidate.ClientDetails.Name) && !(c.Identifier.Equals(candidate.Identifier))));
		}

		#endregion
	}
}

