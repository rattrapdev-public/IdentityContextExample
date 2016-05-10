namespace Geonetric.Identity.Domain.Applications
{
    using System.Collections.Generic;

    public interface IAppRepository
	{
		void Store(App application);
		App GetByIdentifier(AppIdentifier identifier);
		IReadOnlyList<AppSearchResult> GetAll();
	}
}

