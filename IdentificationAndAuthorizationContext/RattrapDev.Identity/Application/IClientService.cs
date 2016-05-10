namespace Geonetric.Identity.Application
{
    using System;
    using System.Collections.Generic;

    using Geonetric.DDD.Application;

    public interface IClientService : IApplicationService
	{
		ClientViewModel SaveNewClient(ClientViewModel viewModel);
		ClientViewModel UpdateClient (ClientViewModel viewModel);
		IReadOnlyList<ClientViewModel> GetAll();
		ClientViewModel ActivateClient(Guid clientIdentity);
		ClientViewModel GetClient(Guid clientIdentity);
	}
}

