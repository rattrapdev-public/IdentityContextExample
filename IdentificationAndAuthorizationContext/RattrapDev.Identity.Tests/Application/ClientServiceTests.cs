using System;
using NUnit.Framework;
using System.Collections.Generic;
using RattrapDev.Identity.Infrastructure;
using System.Linq;
using Shouldly;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class ClientServiceTests
	{
		private const string ClientName = "Acme";
		private const string ContactName = "Wily E. Coyote";
		private const string Phone = "1234567890";
		private const string Email = "joseph@joe.com";

		private const string UpdatedContactName = "Bugs Bunny";
		private const string UpdatedClientName = "Acme Corp";
		private const string UpdatedPhone = "5558675309";
		private const string UpdatedEmail = "joey@joe.com";

		[Test]
		public void SavesNewClientWithGivenValues() 
		{
			IClientService service = new ClientService ();
			var client = service.SaveNewClient (new ClientViewModel { ClientName = ClientName, ContactName = ContactName, ContactPhone = Phone, ContactEmail = Email });
			Assert.That (client.ClientName, Is.EqualTo (ClientName));
			Assert.That (client.Status, Is.EqualTo (ClientStatus.SignedUp.ToString ()));
			Assert.That (client.ContactName, Is.EqualTo (ContactName));
			Assert.That (client.ContactPhone, Is.EqualTo (Phone));
		}

		[Test]
		public void SavingNewClientWithExistingNameThrowsException() 
		{
			IClientService service = new ClientService ();
			var client = service.GetAll ().First ();
			var clientName = client.ClientName;
			Assert.Throws<DuplicateClientException>(() => service.SaveNewClient (new ClientViewModel { ClientName = clientName, ContactName = ContactName, ContactPhone = Phone, ContactEmail = Email }));
		}

		[Test]
		public void UpdatesClientWithGivenValues() 
		{
			IClientService service = new ClientService ();
			var clientName = ClientName + Guid.NewGuid ();
			var client = service.SaveNewClient (new ClientViewModel { ClientName = clientName, ContactName = ContactName, ContactPhone = Phone, ContactEmail = Email });
			var updatedClient = service.UpdateClient (new ClientViewModel { ClientIdentity = client.ClientIdentity, ClientName = UpdatedClientName, ContactName = UpdatedContactName, ContactPhone = UpdatedPhone, ContactEmail = UpdatedEmail });
			Assert.That (updatedClient.ClientName, Is.EqualTo (UpdatedClientName));
			Assert.That (updatedClient.Status, Is.EqualTo (ClientStatus.SignedUp.ToString ()));
			Assert.That (updatedClient.ContactName, Is.EqualTo (UpdatedContactName));
			Assert.That (updatedClient.ContactPhone, Is.EqualTo (UpdatedPhone));
		}

		public void UpdateClient_duplicate_client_name_throws_exception() 
		{
			var service = new ClientService ();
			var clientName = ClientName + Guid.NewGuid ();
			var client = service.SaveNewClient (new ClientViewModel { ClientName = clientName, ContactName = ContactName, ContactPhone = Phone, ContactEmail = Email });
			Should.Throw<DuplicateClientException>(() => service.UpdateClient(new ClientViewModel { ClientIdentity = client.ClientIdentity, ClientName = clientName, ContactName = UpdatedContactName, ContactPhone = UpdatedPhone, ContactEmail = UpdatedEmail }));
		}

		public void UpdateClient_duplicate_contactEmail_throws_exception() 
		{
			var service = new ClientService ();
			var clientName = ClientName + Guid.NewGuid ();
			var client = service.SaveNewClient (new ClientViewModel { ClientName = clientName, ContactName = ContactName, ContactPhone = Phone, ContactEmail = Email });
			Should.Throw<DuplicateContactEmailException>(() => service.UpdateClient(new ClientViewModel { ClientIdentity = client.ClientIdentity, ClientName = UpdatedClientName, ContactName = UpdatedContactName, ContactPhone = UpdatedPhone, ContactEmail = Email }));
		}

		[Test]
		public void GetsByIdentifier()
		{
			IClientService service = new ClientService ();
			var clientName = ClientName + Guid.NewGuid ();
			var client = service.SaveNewClient (new ClientViewModel { ClientName = clientName, ContactName = ContactName, ContactPhone = Phone, ContactEmail = Email });
			var retrievedClient = service.GetClient (client.ClientIdentity);
			Assert.That (retrievedClient.ClientName, Is.EqualTo (clientName));
			Assert.That (retrievedClient.Status, Is.EqualTo (ClientStatus.SignedUp.ToString ()));
			Assert.That (retrievedClient.ContactName, Is.EqualTo (ContactName));
			Assert.That (retrievedClient.ContactPhone, Is.EqualTo (Phone));
		}	

		[Test]
		public void ActivateSetsStatus()
		{
			IClientService service = new ClientService ();
			var clientName = ClientName + Guid.NewGuid ();
			var client = service.SaveNewClient (new ClientViewModel { ClientName = clientName, ContactName = ContactName, ContactPhone = Phone, ContactEmail = Email });
			var retrievedClient = service.ActivateClient (client.ClientIdentity);
			Assert.That (retrievedClient.Status, Is.EqualTo (ClientStatus.Online.ToString ()));
		}	

		[Test]
		public void GetAllReturnsAllClients() 
		{
			ClientInMemoryRepository.ClearDictionary ();
			var clientService = new ClientService ();
			clientService.SaveNewClient (new ClientViewModel { ClientName = ClientName + Guid.NewGuid().ToString(), ContactName = ContactName, ContactPhone = Phone, ContactEmail = "0" + Email });
			clientService.SaveNewClient (new ClientViewModel { ClientName = ClientName + Guid.NewGuid().ToString(), ContactName = ContactName, ContactPhone = Phone, ContactEmail = "1" + Email });
			clientService.SaveNewClient (new ClientViewModel { ClientName = ClientName + Guid.NewGuid().ToString(), ContactName = ContactName, ContactPhone = Phone, ContactEmail = "2" + Email });
			clientService.SaveNewClient (new ClientViewModel { ClientName = ClientName + Guid.NewGuid().ToString(), ContactName = ContactName, ContactPhone = Phone, ContactEmail = "3" + Email });
			var clientList = clientService.GetAll ();
			Assert.That (clientList.Count, Is.EqualTo (6));
		}
	}
}

