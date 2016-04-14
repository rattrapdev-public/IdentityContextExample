using System;
using System.Collections.Generic;

namespace RattrapDev.Identity.Domain.Clients
{
	public interface IClientRepository
	{
		Client GetBy(ClientIdentifier identifier);
		void Store(Client client);
		IReadOnlyList<Client> All();
	}
}

