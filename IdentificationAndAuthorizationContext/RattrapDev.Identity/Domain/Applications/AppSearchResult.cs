using System;
using RattrapDev.DDD;

namespace RattrapDev.Identity.Domain.Applications
{
	public class AppSearchResult : IValueObject
	{
		public AppSearchResult (Guid applicationId, string name)
		{
			ApplicationId = applicationId;
			Name = name;
		}

		public Guid ApplicationId { get; private set; }
		public string Name { get; private set; }
	}
}

