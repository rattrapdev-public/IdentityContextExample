using System;
using RattrapDev.Identity.Domain.Client;
using System.Collections.Generic;

namespace RattrapDev.Identity
{
	public interface IClientRepository
	{
		Client GetBy(ClientIdentifier identifier);
		void Store(Client client);
		IReadOnlyList<Client> All();
	}
}

