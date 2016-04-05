using System;
using NUnit.Framework;
using RattrapDev.Identity.Infrastructure.Applications;
using RattrapDev.Identity.Domain.Applications;
using Shouldly;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class ApplicationInMemoryRepositoryTests
	{
		[Test]
		public void StoreAndGetByIdentifier_stores_and_gets_application() 
		{
			var repository = new ApplicationInMemoryRepository ();
			var application = new Application ("Name", "Description", "http://www.test.com");
			repository.Store (application);
			var reconstitutedApplication = repository.GetByIdentifier (application.Identifier);
			application.Identifier.ShouldBe (reconstitutedApplication.Identifier);
			application.Metadata.ShouldBe (reconstitutedApplication.Metadata);
			application.Url.ShouldBe (reconstitutedApplication.Url);
		}
	}
}

