using System;
using RattrapDev.DDD;
using RattrapDev.Identity.Domain.Client;
using System.Collections.Generic;

namespace RattrapDev.Identity
{
	public interface IClientService : IAppService
	{
		ClientViewModel SaveNewClient(ClientViewModel viewModel);
		ClientViewModel UpdateClient (ClientViewModel viewModel);
		IReadOnlyList<ClientViewModel> GetAll();
		ClientViewModel ActivateClient(Guid clientIdentity);
		ClientViewModel GetClient(Guid clientIdentity);
	}
}

