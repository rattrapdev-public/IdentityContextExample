namespace Geonetric.Identity.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Geonetric.Identity.Domain.Clients;
    using Geonetric.Identity.Infrastructure.Client;

    public class ClientService : IClientService
	{
		private IClientRepository repository = new ClientInMemoryRepository();

		public ClientService() 
		{
		}

		public ClientViewModel SaveNewClient (ClientViewModel viewModel)
		{
			var client = new Client (viewModel.ClientName, viewModel.ContactName, viewModel.ContactPhone, viewModel.ContactEmail);

			if (!((new ClientNameUniqueSpecification(this.repository).IsSatisifiedBy(client)))) 
			{
				throw new DuplicateClientException ();
			}

			if (!((new ContactEmailUniqueSpecification (this.repository).IsSatisifiedBy (client)))) 
			{
				throw new DuplicateContactEmailException ();
			}

			this.repository.Store (client);
			return new ClientViewModel(client);
		}

		public ClientViewModel UpdateClient (ClientViewModel viewModel)
		{
			Client clientToUpdate = this.repository.GetBy (new ClientIdentifier (viewModel.ClientIdentity));
			clientToUpdate.UpdateClientDetails (viewModel.ClientName);
			clientToUpdate.UpdateClientContactInfo (viewModel.ContactName, viewModel.ContactPhone, viewModel.ContactEmail);

			if (!((new ClientNameUniqueSpecification(this.repository).IsSatisifiedBy(clientToUpdate)))) 
			{
				throw new DuplicateClientException ();
			}

			if (!((new ContactEmailUniqueSpecification (this.repository).IsSatisifiedBy (clientToUpdate)))) 
			{
				throw new DuplicateContactEmailException ();
			}

			this.repository.Store (clientToUpdate);
			return new ClientViewModel(clientToUpdate);
		}

		public IReadOnlyList<ClientViewModel> GetAll ()
		{
			return this.repository.All ().Select (c => new ClientViewModel(c)).ToList();
		}

		public ClientViewModel ActivateClient (Guid clientIdentity)
		{
			Client clientToActivate = this.repository.GetBy (new ClientIdentifier (clientIdentity));
			clientToActivate.Activate ();
			this.repository.Store (clientToActivate);
			return new ClientViewModel(clientToActivate);
		}

		public ClientViewModel GetClient (Guid clientIdentity)
		{
			var client = this.repository.GetBy (new ClientIdentifier (clientIdentity));
			return new ClientViewModel(client);
		}
	}
}

