using System;
using RattrapDev.Identity.Domain.Applications;
using System.Collections.Generic;

namespace RattrapDev.Identity.Application
{
	public interface IAppService
	{
		IReadOnlyList<AppSearchResult> GetAllApps();
		AppViewModel GetApp(Guid appId);
		void SaveApp(AppViewModel viewModel);
	}
}

