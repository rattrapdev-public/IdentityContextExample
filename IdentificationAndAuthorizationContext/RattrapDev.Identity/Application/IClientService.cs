using System;
using RattrapDev.DDD;
using System.Collections.Generic;

namespace RattrapDev.Identity
{
	public interface IClientService : IApplicationService
	{
		ClientViewModel SaveNewClient(ClientViewModel viewModel);
		ClientViewModel UpdateClient (ClientViewModel viewModel);
		IReadOnlyList<ClientViewModel> GetAll();
		ClientViewModel ActivateClient(Guid clientIdentity);
		ClientViewModel GetClient(Guid clientIdentity);
	}
}

