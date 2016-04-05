using System;
using System.Collections.Generic;

namespace RattrapDev.Identity.Domain.Applications
{
	public interface IApplicationRepository
	{
		void Store(Application application);
		Application GetByIdentifier(ApplicationIdentifier identifier);
		IReadOnlyList<Application> GetAll();
	}
}

