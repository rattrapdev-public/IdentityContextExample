using System;

namespace RattrapDev.Identity.Domain.Application
{
	public interface IApplicationRepository
	{
		void Store(Application application);
		ApplicationId GetByIdentifier(ApplicationIdentifier identifier);
	}
}

