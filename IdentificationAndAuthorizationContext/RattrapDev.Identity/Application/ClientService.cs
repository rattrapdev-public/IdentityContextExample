using System;
using RattrapDev.Identity.Domain.Client;
using RattrapDev.Identity.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace RattrapDev.Identity
{
	public class ClientService : IClientService
	{
		private IClientRepository repository = new ClientInMemoryRepository();

		public ClientService() 
		{
		}

		public dynamic SaveNewClient (string clientName, string contactName, string contactPhone)
		{
			var client = new Client (clientName, contactName, contactPhone);
			if (!((new ClientNameUniqueSpecification(repository).IsSatisifiedBy(client)))) 
			{
				throw new DuplicateClientException ();
			}
			repository.Store (client);
			return ClientPresentationObjectFactory.CreatePresentationObjectFrom (client);
		}

		public dynamic UpdateClient (Guid clientIdentity, string clientName, string contactName, string contactPhone)
		{
			Client clientToUpdate = repository.GetBy (new ClientIdentifier (clientIdentity));
			clientToUpdate.UpdateClientDetails (clientName);
			clientToUpdate.UpdateClientContactInfo (contactName, contactPhone);
			repository.Store (clientToUpdate);
			return ClientPresentationObjectFactory.CreatePresentationObjectFrom (clientToUpdate);
		}

		public IReadOnlyList<dynamic> GetAll ()
		{
			return repository.All ().Select (c => ClientPresentationObjectFactory.CreatePresentationObjectFrom (c)).ToList();
		}

		public dynamic ActivateClient (Guid clientIdentity)
		{
			Client clientToActivate = repository.GetBy (new ClientIdentifier (clientIdentity));
			clientToActivate.Activate ();
			repository.Store (clientToActivate);
			return ClientPresentationObjectFactory.CreatePresentationObjectFrom(clientToActivate);
		}

		public dynamic GetClient (Guid clientIdentity)
		{
			return ClientPresentationObjectFactory.CreatePresentationObjectFrom(repository.GetBy (new ClientIdentifier (clientIdentity)));
		}
	}
}

