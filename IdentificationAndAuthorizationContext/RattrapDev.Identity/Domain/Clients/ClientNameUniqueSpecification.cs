using System;
using System.Linq;
using RattrapDev.DDD;

namespace RattrapDev.Identity.Domain.Clients
{
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
			return !(repository.All().Any (c => c.ClientDetails.Name.Equals(candidate.ClientDetails.Name) && !(c.Identifier.Equals(candidate.Identifier))));
		}

		#endregion
	}
}

