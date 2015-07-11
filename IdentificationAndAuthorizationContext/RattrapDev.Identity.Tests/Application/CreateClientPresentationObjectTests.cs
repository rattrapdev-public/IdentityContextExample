using System;
using NUnit.Framework;
using RattrapDev.Identity.Domain.Client;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class CreateClientPresentationObjectTests
	{
		[Test]
		public void FactoryCreatesPresentationObject() 
		{
			Client client = new Client ("Acme", "Joe", "1234567890");
			dynamic presentationObject = ClientPresentationObjectFactory.CreatePresentationObjectFrom (client);
			Assert.AreEqual (client.Identifier.Identity, presentationObject.Identity);
			Assert.AreEqual (client.ClientDetails.Name, presentationObject.ClientName);
			Assert.AreEqual (client.ClientDetails.Status.ToString (), presentationObject.Status);
			Assert.AreEqual (client.ContactInfo.Name, presentationObject.ContactName);
			Assert.AreEqual (client.ContactInfo.Phone, presentationObject.ContactPhone);
		}
	}
}

