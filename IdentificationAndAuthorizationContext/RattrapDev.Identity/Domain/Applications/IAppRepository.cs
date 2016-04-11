using System;
using System.Collections.Generic;

namespace RattrapDev.Identity.Domain.Applications
{
	public interface IAppRepository
	{
		void Store(App application);
		App GetByIdentifier(AppIdentifier identifier);
		IReadOnlyList<AppSearchResult> GetAll();
	}
}

