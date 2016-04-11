using System;
using NUnit.Framework;
using RattrapDev.Identity.Infrastructure;
using RattrapDev.Identity.Domain.Client;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class ClientNameUniqueSpecificationTests
	{
		[Test]
		public void ReturnsTrueWithUniqueClientName() 
		{
			var repository = new ClientInMemoryRepository ();
			var specification = new ClientNameUniqueSpecification (repository);

			Client client = new Client (Guid.NewGuid ().ToString (), "John Doe", "1234567890", "joe@joe.com");
			Assert.IsTrue (specification.IsSatisifiedBy (client));
		}

		[Test]
		public void ReturnsFalseWithDuplicateUniqueClient() 
		{
			var repository = new ClientInMemoryRepository ();
			var specification = new ClientNameUniqueSpecification (repository);

			Client client = new Client ("Duplicate Client", "John Doe", "1234567890", "joe@joe.com");
			repository.Store (client);
			Client duplicateClient = new Client ("Duplicate Client", "John Doe", "1234567890", "joe@joe.com");

			Assert.IsFalse (specification.IsSatisifiedBy (duplicateClient));
		}

		[Test]
		public void ReturnsTrueWithDuplicateExistingClient() 
		{
			var repository = new ClientInMemoryRepository ();
			var specification = new ClientNameUniqueSpecification (repository);

			Client client = new Client ("Duplicate Client", "John Doe", "1234567890", "joe@joe.com");
			repository.Store (client);

			Assert.IsTrue (specification.IsSatisifiedBy (client));
		}
	}
}

