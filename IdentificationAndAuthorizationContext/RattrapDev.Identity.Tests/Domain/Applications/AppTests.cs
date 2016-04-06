using System;
using NUnit.Framework;
using RattrapDev.Identity.Domain.Applications;
using Shouldly;
using RattrapDev.Identity.Infrastructure.Applications;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class AppTests
	{
		[Test]
		public void Constructor_creates_new_Application() 
		{
			var application = new App ("name", "description", "http://www.test.com");
			application.Identifier.Id.ShouldNotBe (Guid.Empty);
			application.Metadata.Name.ShouldBe ("name");
			application.Metadata.Description.ShouldBe ("description");
			application.Url.BaseUrl.ShouldBe ("http://www.test.com");
		}

		[Test]
		public void Constructor_reconstitutes_Application_with_dto() 
		{
			var dto = new ApplicationDto {
				Id = Guid.NewGuid (),
				Name = "Name",
				Description = "Description",
				BaseUrl = "http://www.test.com"
			};

			var application = new App (dto);
			application.Identifier.Id.ShouldBe (dto.Id);
			application.Metadata.Name.ShouldBe (dto.Name);
			application.Metadata.Description.ShouldBe (dto.Description);
			application.Url.BaseUrl.ShouldBe (dto.BaseUrl);
		}

		[Test]
		public void UpdateMetadata_updates_application_metadata() 
		{
			var application = new App ("name", "description", "http://www.test.com");
			application.UpdateMetadata ("Updated Name", "Updated Description");
			application.Metadata.Name.ShouldBe ("Updated Name");
			application.Metadata.Description.ShouldBe ("Updated Description");
		}

		[Test]
		public void UpdateUrl_updates_the_url() 
		{
			var application = new App ("name", "description", "http://www.test.com");
			application.UpdateUrl ("http://www.anothertest.com");
			application.Url.BaseUrl .ShouldBe("http://www.anothertest.com");
		}
	}
}

