using System;
using NUnit.Framework;
using RattrapDev.Identity.Domain.Client;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class ClientIdentifierTests
	{
		[Test]
		public void ConstructorGeneratesValue() 
		{
			ClientIdentifier identifier = new ClientIdentifier ();
			Assert.AreNotEqual (identifier.Identity, Guid.Empty);
		}

		[Test]
		public void ConstructorSetsValue() 
		{
			Guid identity = Guid.NewGuid ();
			ClientIdentifier identifier = new ClientIdentifier (identity);
			Assert.AreEqual (identifier.Identity, identity);
		}

		[Test]
		public void EmptyGuidThrowsException() 
		{
			Assert.Throws<ArgumentException> (() => new ClientIdentifier(Guid.Empty));
		}

		[Test]
		public void DifferentObjectsCanStillBeEqual() 
		{
			Guid identity = Guid.NewGuid ();
			ClientIdentifier identifier1 = new ClientIdentifier (identity);
			ClientIdentifier identifier2 = new ClientIdentifier (identity);
			Assert.AreEqual (identifier1, identifier2);
		}
	}
}

