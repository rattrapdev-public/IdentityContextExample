namespace Geonetric.Identity.Application
{
    using System;
    using System.Collections.Generic;

    using Geonetric.Identity.Domain.Applications;

    public class AppService : IAppService
	{
		private IAppRepository repository;

		public AppService(IAppRepository repository) 
		{
			this.repository = repository;
		}

		public IReadOnlyList<AppSearchResult> GetAllApps ()
		{
			return this.repository.GetAll ();
		}

		public AppViewModel GetApp (Guid appId)
		{
			var app = this.repository.GetByIdentifier (new AppIdentifier(appId));
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
				app = this.repository.GetByIdentifier (new AppIdentifier(viewModel.Id));
				app.UpdateMetadata (viewModel.Name, viewModel.Description);
				app.UpdateUrl (viewModel.Url);
			}

			this.repository.Store (app);

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

