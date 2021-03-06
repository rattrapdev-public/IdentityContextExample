﻿using System;
using RattrapDev.Identity.Domain.Clients;
using RattrapDev.Identity.Infrastructure.Clients;
using System.Collections.Generic;
using System.Linq;

namespace RattrapDev.Identity.Application
{
	public class ClientService : IClientService
	{
		private IClientRepository repository = new ClientInMemoryRepository();

		public ClientService() 
		{
		}

		public ClientViewModel SaveNewClient (ClientViewModel viewModel)
		{
			var client = new Client (viewModel.ClientName, viewModel.ContactName, viewModel.ContactPhone, viewModel.ContactEmail);

			if (!((new ClientNameUniqueSpecification(repository).IsSatisifiedBy(client)))) 
			{
				throw new DuplicateClientException ();
			}

			if (!((new ContactEmailUniqueSpecification (repository).IsSatisifiedBy (client)))) 
			{
				throw new DuplicateContactEmailException ();
			}

			repository.Store (client);
			return new ClientViewModel(client);
		}

		public ClientViewModel UpdateClient (ClientViewModel viewModel)
		{
			Client clientToUpdate = repository.GetBy (new ClientIdentifier (viewModel.ClientIdentity));
			clientToUpdate.UpdateClientDetails (viewModel.ClientName);
			clientToUpdate.UpdateClientContactInfo (viewModel.ContactName, viewModel.ContactPhone, viewModel.ContactEmail);

			if (!((new ClientNameUniqueSpecification(repository).IsSatisifiedBy(clientToUpdate)))) 
			{
				throw new DuplicateClientException ();
			}

			if (!((new ContactEmailUniqueSpecification (repository).IsSatisifiedBy (clientToUpdate)))) 
			{
				throw new DuplicateContactEmailException ();
			}

			repository.Store (clientToUpdate);
			return new ClientViewModel(clientToUpdate);
		}

		public IReadOnlyList<ClientViewModel> GetAll ()
		{
			return repository.All ().Select (c => new ClientViewModel(c)).ToList();
		}

		public ClientViewModel ActivateClient (Guid clientIdentity)
		{
			Client clientToActivate = repository.GetBy (new ClientIdentifier (clientIdentity));
			clientToActivate.Activate ();
			repository.Store (clientToActivate);
			return new ClientViewModel(clientToActivate);
		}

		public ClientViewModel GetClient (Guid clientIdentity)
		{
			var client = repository.GetBy (new ClientIdentifier (clientIdentity));
			return new ClientViewModel(client);
		}
	}
}

