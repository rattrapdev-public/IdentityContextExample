using System;
using RattrapDev.DDD;
using RattrapDev.Identity.Domain.Client;
using System.Collections.Generic;

namespace RattrapDev.Identity
{
	public interface IClientService : IApplicationService
	{
		dynamic SaveNewClient(string clientName, string contactName, string contactPhone);
		dynamic UpdateClient (Guid clientIdentity, string clientName, string contactName, string contactPhone);
		IReadOnlyList<dynamic> GetAll();
		dynamic ActivateClient(Guid clientIdentity);
		dynamic GetClient(Guid clientIdentity);
	}
}

