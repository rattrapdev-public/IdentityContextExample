namespace Geonetric.Identity.Application
{
    using System;
    using System.Collections.Generic;

    using Geonetric.Identity.Domain.Applications;

    public interface IAppService
	{
		IReadOnlyList<AppSearchResult> GetAllApps();
		AppViewModel GetApp(Guid appId);
		AppViewModel SaveApp(AppViewModel viewModel);
	}
}

