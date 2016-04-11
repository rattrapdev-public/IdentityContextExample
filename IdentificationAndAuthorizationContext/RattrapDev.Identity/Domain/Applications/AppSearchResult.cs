using System;
using RattrapDev.DDD;

namespace RattrapDev.Identity.Domain.Applications
{
	public class AppSearchResult : IValueObject, IEquatable<AppSearchResult>
	{
		public AppSearchResult (Guid applicationId, string name)
		{
			ApplicationId = applicationId;
			Name = name;
		}

		public Guid ApplicationId { get; private set; }
		public string Name { get; private set; }

		public bool Equals (AppSearchResult other)
		{
			if (other == null)
				return false;
			if (ReferenceEquals (this, other)) 
			{
				return true;
			}

			return this.ApplicationId.Equals (other.ApplicationId)
					&& this.Name.Equals (other.Name);
		}
	}
}

