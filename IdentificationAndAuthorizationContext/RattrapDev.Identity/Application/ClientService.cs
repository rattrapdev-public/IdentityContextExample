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

		public ClientViewModel SaveNewClient (ClientViewModel viewModel)
		{
			var client = new Client (viewModel.ClientName, viewModel.ContactName, viewModel.ContactPhone);
			if (!((new ClientNameUniqueSpecification(repository).IsSatisifiedBy(client)))) 
			{
				throw new DuplicateClientException ();
			}
			repository.Store (client);
			return new ClientViewModel { ClientIdentity = client.Identifier.Identity, ClientName = client.ClientDetails.Name, ContactName = client.ContactInfo.Name, Status = client.ClientDetails.Status.ToString(), ContactPhone = client.ContactInfo.Phone };
		}

		public ClientViewModel UpdateClient (ClientViewModel viewModel)
		{
			Client clientToUpdate = repository.GetBy (new ClientIdentifier (viewModel.ClientIdentity));
			clientToUpdate.UpdateClientDetails (viewModel.ClientName);
			clientToUpdate.UpdateClientContactInfo (viewModel.ContactName, viewModel.ContactPhone);
			repository.Store (clientToUpdate);
			return new ClientViewModel { ClientIdentity = clientToUpdate.Identifier.Identity, ClientName = clientToUpdate.ClientDetails.Name, ContactName = clientToUpdate.ContactInfo.Name, Status = clientToUpdate.ClientDetails.Status.ToString(), ContactPhone = clientToUpdate.ContactInfo.Phone };
		}

		public IReadOnlyList<ClientViewModel> GetAll ()
		{
			return repository.All ().Select (c => new ClientViewModel { ClientIdentity = c.Identifier.Identity, ClientName = c.ClientDetails.Name, ContactName = c.ContactInfo.Name, Status = c.ClientDetails.Status.ToString(), ContactPhone = c.ContactInfo.Phone }).ToList();
		}

		public ClientViewModel ActivateClient (Guid clientIdentity)
		{
			Client clientToActivate = repository.GetBy (new ClientIdentifier (clientIdentity));
			clientToActivate.Activate ();
			repository.Store (clientToActivate);
			return new ClientViewModel { ClientIdentity = clientToActivate.Identifier.Identity, ClientName = clientToActivate.ClientDetails.Name, ContactName = clientToActivate.ContactInfo.Name, Status = clientToActivate.ClientDetails.Status.ToString(), ContactPhone = clientToActivate.ContactInfo.Phone };
		}

		public ClientViewModel GetClient (Guid clientIdentity)
		{
			var client = repository.GetBy (new ClientIdentifier (clientIdentity));
			return new ClientViewModel { ClientIdentity = client.Identifier.Identity, ClientName = client.ClientDetails.Name, ContactName = client.ContactInfo.Name, Status = client.ClientDetails.Status.ToString(), ContactPhone = client.ContactInfo.Phone };
		}
	}
}

