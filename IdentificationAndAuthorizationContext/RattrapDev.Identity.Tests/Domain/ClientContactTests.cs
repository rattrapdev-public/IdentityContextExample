using System;
using NUnit.Framework;
using RattrapDev.Identity.Domain.Client;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class ClientContactTests
	{
		private const string Name = "Joe";
		private const string Phone = "1234567890";

		[Test]
		public void ConstructorSetsValues() 
		{
			ClientContact contact = new ClientContact (Name, Phone);
			Assert.AreEqual (Name, contact.Name);
			Assert.AreEqual (Phone, contact.Phone);
		}

		[Test]
		public void NullOrEmptyNameThrowsException() 
		{
			Assert.Throws<ArgumentException> (() => new ClientContact (null, Phone));
			Assert.Throws<ArgumentException> (() => new ClientContact (string.Empty, Phone));
			Assert.Throws<ArgumentException> (() => new ClientContact (" ", Phone));
		}

		[Test]
		public void NullOrEmptyPhoneThrowsException() 
		{
			Assert.Throws<ArgumentException> (() => new ClientContact (Name, null));
			Assert.Throws<ArgumentException> (() => new ClientContact (Name, string.Empty));
			Assert.Throws<ArgumentException> (() => new ClientContact (Name, " "));
		}

		[Test]
		public void DifferentClientContactsCanStillBeEqual() 
		{
			ClientContact contact1 = new ClientContact (Name, Phone);
			ClientContact contact2 = new ClientContact (Name, Phone);
			Assert.AreEqual (contact1, contact2);
		}
	}
}

