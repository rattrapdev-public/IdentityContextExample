namespace Geonetric.Identity.Domain.Clients
{
    using System.Collections.Generic;

    public interface IClientRepository
	{
		Client GetBy(ClientIdentifier identifier);
		void Store(Client client);
		IReadOnlyList<Client> All();
	}
}

