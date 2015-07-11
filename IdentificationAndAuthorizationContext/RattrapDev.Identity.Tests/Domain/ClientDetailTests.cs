using System;
using NUnit.Framework;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class ClientDetailTests
	{
		private const string Name = "Acme Manufacturing";
		[Test]
		public void ConstructorSetsValue() 
		{
			ClientStatus status = ClientStatus.Lapsed;
			ClientDetails clientDetails = new ClientDetails (Name, status);
			Assert.AreEqual (Name, clientDetails.Name);
			Assert.AreEqual (status, clientDetails.Status);
		}

		[Test]
		public void NullOrWhiteSpaceNameThrowsException() 
		{
			Assert.Throws<ArgumentException> (() => new ClientDetails (null, ClientStatus.Lapsed));
			Assert.Throws<ArgumentException> (() => new ClientDetails (string.Empty, ClientStatus.Lapsed));
			Assert.Throws<ArgumentException> (() => new ClientDetails (" ", ClientStatus.Lapsed));
		}

		[Test]
		public void DifferentClientDetailsCanStillBeEqual() 
		{
			ClientDetails details1 = new ClientDetails (Name, ClientStatus.Lapsed);
			ClientDetails details2 = new ClientDetails (Name, ClientStatus.Lapsed);
			Assert.AreEqual (details1, details2);
		}
	}
}

