namespace Geonetric.Identity.Tests.Domain.Client
{
    using System;

    using Geonetric.Identity.Domain.Clients;

    using NUnit.Framework;

    using Shouldly;

    [TestFixture]
	public class ClientContactTests
	{
		private const string Name = "Joe";
		private const string Phone = "1234567890";
		private const string Email = "joe@joe.com";

		[Test]
		public void ConstructorSetsValues() 
		{
			ClientContact contact = new ClientContact (Name, Phone, Email);
			contact.Name.ShouldBe (Name);
			contact.Phone.ShouldBe (Phone);
			contact.Email.ShouldBe (Email);
		}

		[TestCase("")]
		[TestCase(" ")]
		[TestCase(null)]
		public void NullOrEmptyNameThrowsException(string invalidName) 
		{
			Should.Throw<ArgumentException> (() => new ClientContact (invalidName, Phone, Email));
		}

		[TestCase("")]
		[TestCase(" ")]
		[TestCase(null)]
		public void NullOrEmptyPhoneThrowsException(string invalidPhone) 
		{
			Assert.Throws<ArgumentException> (() => new ClientContact (Name, invalidPhone, Email));
		}

		[TestCase("")]
		[TestCase(" ")]
		[TestCase(null)]
		[TestCase("test@")]
		public void InvalidEmailThrowsException(string invalidEmail) 
		{
			Assert.Throws<ArgumentException> (() => new ClientContact (Name, Phone, invalidEmail));
		}

		[Test]
		public void DifferentClientContactsCanStillBeEqual() 
		{
			ClientContact contact1 = new ClientContact (Name, Phone, Email);
			ClientContact contact2 = new ClientContact (Name, Phone, Email);
			Assert.AreEqual (contact1, contact2);
		}
	}
}

