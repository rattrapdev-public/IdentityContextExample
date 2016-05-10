namespace Geonetric.Identity.Tests.Domain.Client
{
    using System;

    using Geonetric.Identity.Domain.Clients;

    using NUnit.Framework;

    using Shouldly;

    [TestFixture]
	public class ClientTests
	{
		private const string ClientName = "Acme";
		private const string ContactName = "Wily E. Coyote";
		private const string ContactPhone = "1234567890";
		private const string ContactEmail = "joe@joe.com";

		[Test]
		public void ConstructorCreatesNewClient() 
		{
			Client client = new Client (ClientName, ContactName, ContactPhone, ContactEmail);
			client.Identifier.ShouldNotBeNull();
			client.ClientDetails.Name.ShouldBe(ClientName);
			client.ClientDetails.Status.ShouldBe(ClientStatus.SignedUp);
			client.ContactInfo.Name.ShouldBe(ContactName);
			client.ContactInfo.Phone.ShouldBe(ContactPhone);
			client.ContactInfo.Email.ShouldBe (ContactEmail);
		}

		[Test]
		public void ConstructorReconstitutesClient() 
		{
			Guid identity = Guid.NewGuid ();
			ClientStatus currentStatus = ClientStatus.Online;
			Client client = new Client (identity, ClientName, currentStatus, ContactName, ContactPhone, ContactEmail);
			new ClientIdentifier (identity).ShouldBe(client.Identifier);
			new ClientDetails (ClientName, currentStatus).ShouldBe(client.ClientDetails);
			new ClientContact (ContactName, ContactPhone, ContactEmail).ShouldBe(client.ContactInfo);
		}

		[Test]
		public void DifferentClientsCanStillBeEqual() 
		{
			Guid identity = Guid.NewGuid ();
			ClientStatus currentStatus = ClientStatus.Online;
			Client client1 = new Client (identity, ClientName, currentStatus, ContactName, ContactPhone, ContactEmail);
			Client client2 = new Client (identity, ClientName, currentStatus, ContactName, ContactPhone, ContactEmail);
			Assert.AreEqual (client1, client2);
		}

		[Test]
		public void NewClientCanBeActivated() 
		{
			Client client = new Client (ClientName, ContactName, ContactPhone, ContactEmail);
			client.Activate ();
			Assert.AreEqual (client.ClientDetails.Status, ClientStatus.Online);
		}

		[Test]
		public void ActivatingLapsedClientThrowsException() 
		{
			// ToDo: Add functionality.
		}

		[Test]
		public void ActivatingOnlineClientThrowsException() 
		{
			// ToDo: Add functionality.
		}
	}
}

