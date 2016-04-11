using System;
using NUnit.Framework;
using RattrapDev.Identity.Domain.Applications;
using NSubstitute;
using System.Collections.Generic;
using RattrapDev.Identity.Application;
using System.Linq;
using Shouldly;
using RattrapDev.Identity.Infrastructure.Applications;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class AppServiceTests
	{
		[Test]
		public void GetAll_returns_all_appSearchResult_from_repository() 
		{
			var searchResults = new List<AppSearchResult> 
								{
									new AppSearchResult(Guid.NewGuid(), "Name"),
									new AppSearchResult(Guid.NewGuid(), "Another Name")
								};

			var repository = Substitute.For<IAppRepository> ();
			repository.GetAll ().Returns (searchResults);

			var appService = new AppService (repository);

			var retrievedResults = appService.GetAllApps();
			retrievedResults.SequenceEqual (searchResults).ShouldBeTrue ();
		}

		[Test]
		public void GetApp_returns_matching_app_from_repository() 
		{
			var app = new App ("name", "description", "https://www.test.com");

			var repository = Substitute.For<IAppRepository> ();
			repository.GetByIdentifier (Arg.Any<AppIdentifier> ()).Returns (app);

			var appService = new AppService (repository);

			var retrievedApp = appService.GetApp(Guid.NewGuid());
			retrievedApp.Id.Equals (app.Identifier.Id).ShouldBeTrue ();
		}

		[Test]
		public void SaveApp_sends_app_to_repository() 
		{
			var viewModel = new AppViewModel 
							{
								Id = Guid.NewGuid(),
								Name = "Name",
								Description = "Description",
								Url = "http://www.test.com"
							};

			var dto = new AppDto 
			{
				Id = viewModel.Id,
				Name = viewModel.Name,
				Description = viewModel.Description,
				BaseUrl = viewModel.Url
			};

			App app = null;
			var repository = Substitute.For<IAppRepository> ();
			repository.Store (Arg.Do<App> (a => app = a));
			repository.GetByIdentifier (Arg.Any<AppIdentifier>()).Returns (new App (dto));

			var appService = new AppService (repository);

			appService.SaveApp (viewModel);
			app.ShouldNotBeNull ();
			app.Identifier.Id.ShouldBe (viewModel.Id);
			app.Metadata.Name.ShouldBe (viewModel.Name);
			app.Metadata.Description.ShouldBe (viewModel.Description);
			app.Url.BaseUrl.ShouldBe (viewModel.Url);
		}
	}
}

