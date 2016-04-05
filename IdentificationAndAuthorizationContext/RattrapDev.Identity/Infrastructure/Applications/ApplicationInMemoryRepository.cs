using RattrapDev.Identity.Domain.Applications;
using System.Collections.Generic;
using System.Linq;

namespace RattrapDev.Identity.Infrastructure.Applications
{
	public class ApplicationInMemoryRepository : IApplicationRepository
	{
		private IDictionary<ApplicationIdentifier, Application> applicationDictionary = new Dictionary<ApplicationIdentifier, Application> ();

		public void Store (Application application)
		{
			applicationDictionary [application.Identifier] = application;
		}
		public Application GetByIdentifier (ApplicationIdentifier identifier)
		{
			if (applicationDictionary.ContainsKey (identifier)) 
			{
				var application = applicationDictionary [identifier];
				var dto = new ApplicationDto 
				{
					Id = application.Identifier.Id,
					Name = application.Metadata.Name,
					Description = application.Metadata.Description,
					BaseUrl = application.Url.BaseUrl
				};

				return new Application (dto);
			}

			return null;
		}

		public IReadOnlyList<ApplicationSearchResult> GetAll ()
		{
			return applicationDictionary.Values.Select (a => new ApplicationSearchResult (a.Identifier.Id, a.Metadata.Name)).ToList();
		}
	}
}

