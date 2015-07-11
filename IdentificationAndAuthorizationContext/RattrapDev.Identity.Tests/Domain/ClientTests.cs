using System;
using NUnit.Framework;
using RattrapDev.Identity.Domain.Client;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class ClientTests
	{
		private const string ClientName = "Acme";
		private const string ContactName = "Wily E. Coyote";
		private const string ContactPhone = "1234567890";

		[Test]
		public void ConstructorCreatesNewClient() 
		{
			Client client = new Client (ClientName, ContactName, ContactPhone);
			Assert.IsNotNull (client.Identifier);
			Assert.AreEqual (client.ClientDetails.Name, ClientName);
			Assert.AreEqual (client.ClientDetails.Status, ClientStatus.SignedUp);
			Assert.AreEqual (client.ContactInfo.Name, ContactName);
			Assert.AreEqual (client.ContactInfo.Phone, ContactPhone);
		}

		[Test]
		public void ConstructorReconstitutesClient() 
		{
			Guid identity = Guid.NewGuid ();
			ClientStatus currentStatus = ClientStatus.Online;
			Client client = new Client (identity, ClientName, currentStatus, ContactName, ContactPhone);
			Assert.AreEqual (new ClientIdentifier (identity), client.Identifier);
			Assert.AreEqual (new ClientDetails (ClientName, currentStatus), client.ClientDetails);
			Assert.AreEqual (new ClientContact (ContactName, ContactPhone), client.ContactInfo);
		}

		[Test]
		public void DifferentClientsCanStillBeEqual() 
		{
			Guid identity = Guid.NewGuid ();
			ClientStatus currentStatus = ClientStatus.Online;
			Client client1 = new Client (identity, ClientName, currentStatus, ContactName, ContactPhone);
			Client client2 = new Client (identity, ClientName, currentStatus, ContactName, ContactPhone);
			Assert.AreEqual (client1, client2);
		}

		[Test]
		public void NewClientCanBeActivated() 
		{
			Client client = new Client (ClientName, ContactName, ContactPhone);
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

