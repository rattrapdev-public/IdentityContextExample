using System;
using NUnit.Framework;
using RattrapDev.Identity.Infrastructure.Applications;
using RattrapDev.Identity.Domain.Applications;
using Shouldly;
using System.Linq;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class AppInMemoryRepositoryTests
	{
		[Test]
		public void StoreAndGetByIdentifier_stores_and_gets_application() 
		{
			var repository = new AppInMemoryRepository ();
			var application = new App ("Name", "Description", "http://www.test.com");
			repository.Store (application);
			var reconstitutedApplication = repository.GetByIdentifier (application.Identifier);
			application.Identifier.ShouldBe (reconstitutedApplication.Identifier);
			application.Metadata.ShouldBe (reconstitutedApplication.Metadata);
			application.Url.ShouldBe (reconstitutedApplication.Url);
		}

		[Test]
		public void GetAll_returns_preexisting_app() 
		{
			var repository = new AppInMemoryRepository ();
			var result = repository.GetAll().First ();
			var app = repository.GetByIdentifier (new AppIdentifier (result.ApplicationId));
			app.Metadata.Name.ShouldBe (result.Name);
		}
	}
}

