using System;
using RattrapDev.Identity.Domain.Client;
using System.Collections.Generic;

namespace RattrapDev.Identity
{
	public class MockClientService : IClientService
	{
		#region IClientService implementation

		public IReadOnlyList<dynamic> GetAll ()
		{
			var client1 = new Client ("Client 1", "Joe", "1234567890");
			var client2 = new Client ("Client 2", "Jane", "1234567890");
			var client3 = new Client ("Client 3", "Jim", "1234567890");
			var client4 = new Client ("Client 4", "Jenny", "1234567890");
			var client5 = new Client ("Client 5", "John", "1234567890");

			var clients = new List<dynamic> () 
							{ ClientPresentationObjectFactory.CreatePresentationObjectFrom(client1), 
							ClientPresentationObjectFactory.CreatePresentationObjectFrom(client2), 
							ClientPresentationObjectFactory.CreatePresentationObjectFrom(client3), 
							ClientPresentationObjectFactory.CreatePresentationObjectFrom(client4), 
							ClientPresentationObjectFactory.CreatePresentationObjectFrom(client5) };
			return clients;
		}

		public dynamic SaveNewClient (string clientName, string contactName, string contactPhone)
		{
			return ClientPresentationObjectFactory.CreatePresentationObjectFrom(new Client (clientName, contactName, contactPhone));
		}

		public dynamic UpdateClient (Guid clientIdentity, string clientName, string contactName, string contactPhone)
		{
			return ClientPresentationObjectFactory.CreatePresentationObjectFrom(new Client (clientIdentity, clientName, ClientStatus.SignedUp, contactName, contactPhone));
		}

		public dynamic ActivateClient (Guid clientIdentity)
		{
			return ClientPresentationObjectFactory.CreatePresentationObjectFrom(new Client (clientIdentity, "Client 1", ClientStatus.Online, "Joe", "1234567890"));
		}

		public dynamic GetClient (Guid clientIdentity)
		{
			return ClientPresentationObjectFactory.CreatePresentationObjectFrom(new Client (clientIdentity, "Client 1", ClientStatus.SignedUp, "Joe", "1234567890"));
		}
		#endregion
	}
}

