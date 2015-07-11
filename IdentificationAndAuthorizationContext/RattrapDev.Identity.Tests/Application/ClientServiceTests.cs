using System;
using NUnit.Framework;
using System.Collections.Generic;
using RattrapDev.Identity.Infrastructure;
using System.Linq;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class ClientServiceTests
	{
		private const string ClientName = "Acme";
		private const string ContactName = "Wily E. Coyote";
		private const string Phone = "1234567890";

		private const string UpdatedContactName = "Bugs Bunny";
		private const string UpdatedClientName = "Acme Corp";
		private const string UpdatedPhone = "5558675309";

		private IClientService service = new ClientService ();

		[Test]
		public void SavesNewClientWithGivenValues() 
		{
			dynamic client = service.SaveNewClient (ClientName, ContactName, Phone);
			Assert.That (client.ClientName, Is.EqualTo (ClientName));
			Assert.That (client.Status, Is.EqualTo (ClientStatus.SignedUp.ToString ()));
			Assert.That (client.ContactName, Is.EqualTo (ContactName));
			Assert.That (client.ContactPhone, Is.EqualTo (Phone));
		}

		[Test]
		public void SavingNewClientWithExistingNameThrowsException() 
		{
			dynamic client = service.GetAll ().First ();
			var clientName = client.ClientDetails.Name;
			Assert.Throws<DuplicateClientException>(() => service.SaveNewClient (clientName, ContactName, Phone));
		}

		[Test]
		public void UpdatesClientWithGivenValues() 
		{
			var clientName = ClientName + Guid.NewGuid ();
			dynamic client = service.SaveNewClient (clientName, ContactName, Phone);
			dynamic updatedClient = service.UpdateClient (client.Identity, UpdatedClientName, UpdatedContactName, UpdatedPhone);
			Assert.That (updatedClient.ClientName, Is.EqualTo (UpdatedClientName));
			Assert.That (updatedClient.Status, Is.EqualTo (ClientStatus.SignedUp.ToString ()));
			Assert.That (updatedClient.ContactName, Is.EqualTo (UpdatedContactName));
			Assert.That (updatedClient.ContactPhone, Is.EqualTo (UpdatedPhone));
		}

		[Test]
		public void GetsByIdentifier()
		{
			var clientName = ClientName + Guid.NewGuid ();
			dynamic client = service.SaveNewClient (clientName, ContactName, Phone);
			dynamic retrievedClient = service.GetClient (client.Identity);
			Assert.That (retrievedClient.ClientName, Is.EqualTo (clientName));
			Assert.That (retrievedClient.Status, Is.EqualTo (ClientStatus.SignedUp.ToString ()));
			Assert.That (retrievedClient.ContactName, Is.EqualTo (ContactName));
			Assert.That (retrievedClient.ContactPhone, Is.EqualTo (Phone));
		}	

		[Test]
		public void ActivateSetsStatus()
		{
			var clientName = ClientName + Guid.NewGuid ();
			dynamic client = service.SaveNewClient (clientName, ContactName, Phone);
			dynamic retrievedClient = service.ActivateClient (client.Identity);
			Assert.That (retrievedClient.Status, Is.EqualTo (ClientStatus.Online.ToString ()));
		}	

		[Test]
		public void GetAllReturnsAllClients() 
		{
			ClientInMemoryRepository.ClearDictionary ();
			IClientService clientService = new ClientService ();
			clientService.SaveNewClient (ClientName + Guid.NewGuid().ToString(), ContactName, Phone);
			clientService.SaveNewClient (ClientName + Guid.NewGuid().ToString(), ContactName, Phone);
			clientService.SaveNewClient (ClientName + Guid.NewGuid().ToString(), ContactName, Phone);
			clientService.SaveNewClient (ClientName + Guid.NewGuid().ToString(), ContactName, Phone);
			IReadOnlyList<dynamic> clientList = clientService.GetAll ();
			Assert.That (clientList.Count, Is.EqualTo (6));
		}
	}
}

