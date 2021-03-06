﻿using NUnit.Framework;
using RattrapDev.Identity.Infrastructure.Clients;
using RattrapDev.Identity.Domain.Clients;
using System;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class ContactEmailUniqueSpecificationTests
	{
		[Test]
		public void ReturnsTrueWithUniqueClientName() 
		{
			var repository = new ClientInMemoryRepository ();
			var specification = new ContactEmailUniqueSpecification (repository);

			Client client = new Client ("Client", "John Doe", "1234567890", "joe@joe.com");
			Assert.IsTrue (specification.IsSatisifiedBy (client));
		}

		[Test]
		public void ReturnsFalseWithDuplicateUniqueClient() 
		{
			var repository = new ClientInMemoryRepository ();
			var specification = new ContactEmailUniqueSpecification (repository);

			Client client = new Client ("Client 1", "John Doe", "1234567890", "duplicate@joe.com");
			repository.Store (client);
			Client duplicateClient = new Client ("Client 2", "John Doe", "1234567890", "duplicate@joe.com");

			Assert.IsFalse (specification.IsSatisifiedBy (duplicateClient));
		}

		[Test]
		public void ReturnsTrueWithDuplicateExistingClient() 
		{
			var repository = new ClientInMemoryRepository ();
			var specification = new ContactEmailUniqueSpecification (repository);

			Client client = new Client ("Client", "John Doe", "1234567890", "existing@joe.com");
			repository.Store (client);

			Assert.IsTrue (specification.IsSatisifiedBy (client));
		}
	}
}

