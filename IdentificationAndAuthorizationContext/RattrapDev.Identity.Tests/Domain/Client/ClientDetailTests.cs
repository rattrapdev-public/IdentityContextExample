using System;
using NUnit.Framework;
using Shouldly;
using RattrapDev.Identity.Domain.Clients;

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
			clientDetails.Name.ShouldBe (Name);
			clientDetails.Status.ShouldBe (status);
		}

		[Test]
		public void NullOrWhiteSpaceNameThrowsException() 
		{
			Should.Throw<ArgumentException> (() => new ClientDetails (null, ClientStatus.Lapsed));
			Should.Throw<ArgumentException> (() => new ClientDetails (string.Empty, ClientStatus.Lapsed));
			Should.Throw<ArgumentException> (() => new ClientDetails (" ", ClientStatus.Lapsed));
		}

		[Test]
		public void DifferentClientDetailsCanStillBeEqual() 
		{
			ClientDetails details1 = new ClientDetails (Name, ClientStatus.Lapsed);
			ClientDetails details2 = new ClientDetails (Name, ClientStatus.Lapsed);
			details1.ShouldBe (details2);
		}
	}
}

