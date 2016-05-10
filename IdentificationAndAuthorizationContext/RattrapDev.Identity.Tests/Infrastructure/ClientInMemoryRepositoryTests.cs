namespace Geonetric.Identity.Tests.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;

    using Geonetric.Identity.Domain.Clients;
    using Geonetric.Identity.Infrastructure.Client;

    using NUnit.Framework;

    [TestFixture]
	public class ClientInMemoryRepositoryTests
	{
		private const string ClientName = "Acme";
		private const string ContactName = "Wily E. Coyote";
		private const string Phone = "1234567890";
		private const string Email = "wily@coyote.com";

		private const string UpdatedContactName = "Bugs Bunny";

		private IClientRepository repository = new ClientInMemoryRepository();

		[Test]
		public void RepositoryReturnsNullForNonexistingClient() 
		{
			Assert.IsNull(this.repository.GetBy(new ClientIdentifier()));
		}

		[Test]
		public void RepositoryStoresAndGetsNewValue() 
		{
			var client = new Client (ClientName, ContactName, Phone, Email);
			Assert.IsNull (this.repository.GetBy (client.Identifier));
			this.repository.Store (client);
			Client reconstitutedClient = this.repository.GetBy (client.Identifier);
			Assert.That (client, Is.EqualTo (reconstitutedClient));
		}

		[Test]
		public void UpdatingClientDoesNotUpdateStoredClient() 
		{
			var client = new Client (ClientName, ContactName, Phone, Email);
			Assert.IsNull (this.repository.GetBy (client.Identifier));
			this.repository.Store (client);
			client.UpdateClientContactInfo (UpdatedContactName, Phone, Email);
			Client reconstitutedClient = this.repository.GetBy (client.Identifier);
			Assert.That (client.ContactInfo.Name, Is.Not.EqualTo (reconstitutedClient.ContactInfo.Name));
		}

		[Test]
		public void StoredClientCanBeUpdated() 
		{
			Client client = new Client (ClientName, ContactName, Phone, Email);
			Assert.IsNull (this.repository.GetBy (client.Identifier));
			this.repository.Store (client);
			client.UpdateClientContactInfo (UpdatedContactName, Phone, Email);
			this.repository.Store (client);
			Client reconstitutedClient = this.repository.GetBy (client.Identifier);
			Assert.That (client.ContactInfo.Name, Is.EqualTo (reconstitutedClient.ContactInfo.Name));
		}

		[Test]
		public void AllGetsAllValues() 
		{
			ClientInMemoryRepository clientRepository = new ClientInMemoryRepository ();
			ClientInMemoryRepository.ClearDictionary ();
			var client1 = new Client (ClientName, ContactName, Phone, Email);
			var client2 = new Client (ClientName, ContactName, Phone, Email);
			var client3 = new Client (ClientName, ContactName, Phone, Email);
			clientRepository.Store (client1);
			clientRepository.Store (client2);
			clientRepository.Store (client3);
			IReadOnlyList<Client> clientList = clientRepository.All ();
			Assert.That (clientList.Count, Is.EqualTo (5));
			Assert.IsTrue (clientList.Any (c => c.Identifier.Equals (client1.Identifier)));
			Assert.IsTrue (clientList.Any (c => c.Identifier.Equals (client2.Identifier)));
			Assert.IsTrue (clientList.Any (c => c.Identifier.Equals (client3.Identifier)));
		}
	}
}

