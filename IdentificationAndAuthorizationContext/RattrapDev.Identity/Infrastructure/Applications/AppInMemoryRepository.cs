using RattrapDev.Identity.Domain.Applications;
using System.Collections.Generic;
using System.Linq;

namespace RattrapDev.Identity.Infrastructure.Applications
{
	public class AppInMemoryRepository : IAppRepository
	{
		private IDictionary<AppIdentifier, App> applicationDictionary = new Dictionary<AppIdentifier, App> ();

		public AppInMemoryRepository() 
		{
			var app = new App ("Acme Invention Creator", "The Acme Invention Creator.", "http://www.acme.com");
			applicationDictionary.Add (app.Identifier, app);
			app = new App ("Wayne Enterprises", string.Empty, "http://www.wayneenterprises.com");
			applicationDictionary.Add (app.Identifier, app);
		}

		public void Store (App application)
		{
			applicationDictionary [application.Identifier] = application;
		}

		public App GetByIdentifier (AppIdentifier identifier)
		{
			if (applicationDictionary.ContainsKey (identifier)) 
			{
				var application = applicationDictionary [identifier];
				var dto = new AppDto 
				{
					Id = application.Identifier.Id,
					Name = application.Metadata.Name,
					Description = application.Metadata.Description,
					BaseUrl = application.Url.BaseUrl
				};

				return new App (dto);
			}

			return null;
		}

		public IReadOnlyList<AppSearchResult> GetAll ()
		{
			return applicationDictionary.Values.Select (a => new AppSearchResult (a.Identifier.Id, a.Metadata.Name)).ToList();
		}
	}
}

