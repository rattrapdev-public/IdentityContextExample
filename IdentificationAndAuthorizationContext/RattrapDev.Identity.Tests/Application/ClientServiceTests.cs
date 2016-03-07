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
			var client = service.SaveNewClient (new ClientViewModel { ClientName = ClientName, ContactName = ContactName, ContactPhone = Phone });
			Assert.That (client.ClientName, Is.EqualTo (ClientName));
			Assert.That (client.Status, Is.EqualTo (ClientStatus.SignedUp.ToString ()));
			Assert.That (client.ContactName, Is.EqualTo (ContactName));
			Assert.That (client.ContactPhone, Is.EqualTo (Phone));
		}

		[Test]
		public void SavingNewClientWithExistingNameThrowsException() 
		{
			var client = service.GetAll ().First ();
			var clientName = client.ClientName;
			Assert.Throws<DuplicateClientException>(() => service.SaveNewClient (new ClientViewModel { ClientName = clientName, ContactName = ContactName, ContactPhone = Phone }));
		}

		[Test]
		public void UpdatesClientWithGivenValues() 
		{
			var clientName = ClientName + Guid.NewGuid ();
			var client = service.SaveNewClient (new ClientViewModel { ClientName = clientName, ContactName = ContactName, ContactPhone = Phone });
			var updatedClient = service.UpdateClient (new ClientViewModel { ClientIdentity = client.ClientIdentity, ClientName = UpdatedClientName, ContactName = UpdatedContactName, ContactPhone = UpdatedPhone });
			Assert.That (updatedClient.ClientName, Is.EqualTo (UpdatedClientName));
			Assert.That (updatedClient.Status, Is.EqualTo (ClientStatus.SignedUp.ToString ()));
			Assert.That (updatedClient.ContactName, Is.EqualTo (UpdatedContactName));
			Assert.That (updatedClient.ContactPhone, Is.EqualTo (UpdatedPhone));
		}

		[Test]
		public void GetsByIdentifier()
		{
			var clientName = ClientName + Guid.NewGuid ();
			var client = service.SaveNewClient (new ClientViewModel { ClientName = clientName, ContactName = ContactName, ContactPhone = Phone });
			var retrievedClient = service.GetClient (client.ClientIdentity);
			Assert.That (retrievedClient.ClientName, Is.EqualTo (clientName));
			Assert.That (retrievedClient.Status, Is.EqualTo (ClientStatus.SignedUp.ToString ()));
			Assert.That (retrievedClient.ContactName, Is.EqualTo (ContactName));
			Assert.That (retrievedClient.ContactPhone, Is.EqualTo (Phone));
		}	

		[Test]
		public void ActivateSetsStatus()
		{
			var clientName = ClientName + Guid.NewGuid ();
			var client = service.SaveNewClient (new ClientViewModel { ClientName = clientName, ContactName = ContactName, ContactPhone = Phone });
			var retrievedClient = service.ActivateClient (client.ClientIdentity);
			Assert.That (retrievedClient.Status, Is.EqualTo (ClientStatus.Online.ToString ()));
		}	

		[Test]
		public void GetAllReturnsAllClients() 
		{
			ClientInMemoryRepository.ClearDictionary ();
			var clientService = new ClientService ();
			clientService.SaveNewClient (new ClientViewModel { ClientName = ClientName + Guid.NewGuid().ToString(), ContactName = ContactName, ContactPhone = Phone });
			clientService.SaveNewClient (new ClientViewModel { ClientName = ClientName + Guid.NewGuid().ToString(), ContactName = ContactName, ContactPhone = Phone });
			clientService.SaveNewClient (new ClientViewModel { ClientName = ClientName + Guid.NewGuid().ToString(), ContactName = ContactName, ContactPhone = Phone });
			clientService.SaveNewClient (new ClientViewModel { ClientName = ClientName + Guid.NewGuid().ToString(), ContactName = ContactName, ContactPhone = Phone });
			var clientList = clientService.GetAll ();
			Assert.That (clientList.Count, Is.EqualTo (6));
		}
	}
}

