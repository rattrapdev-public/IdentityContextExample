using System;
using RattrapDev.Identity.Domain.Applications;
using System.Collections.Generic;

namespace RattrapDev.Identity.Application
{
	public class AppService : IAppService
	{
		private IAppRepository repository;

		public AppService(IAppRepository repository) 
		{
			this.repository = repository;
		}

		public IReadOnlyList<AppSearchResult> GetAllApps ()
		{
			return repository.GetAll ();
		}

		public AppViewModel GetApp (Guid appId)
		{
			var app = repository.GetByIdentifier (new AppIdentifier(appId));
			return new AppViewModel 
			{
				Id = app.Identifier.Id,
				Name = app.Metadata.Name,
				Description = app.Metadata.Description,
				Url = app.Url.BaseUrl
			};
		}

		public AppViewModel SaveApp (AppViewModel viewModel)
		{
			App app;
			if (viewModel.Id.Equals (Guid.Empty)) 
			{
				app = new App (viewModel.Name, viewModel.Description, viewModel.Url);
			} 
			else 
			{
				app = repository.GetByIdentifier (new AppIdentifier(viewModel.Id));
				app.UpdateMetadata (viewModel.Name, viewModel.Description);
				app.UpdateUrl (viewModel.Url);
			}

			repository.Store (app);

			return new AppViewModel 
			{
				Id = app.Identifier.Id,
				Name = app.Metadata.Name,
				Description = app.Metadata.Description,
				Url = app.Url.BaseUrl
			};
		}
	}
}

