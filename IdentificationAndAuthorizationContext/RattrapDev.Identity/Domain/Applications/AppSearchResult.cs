namespace Geonetric.Identity.Domain.Applications
{
    using System;

    using Geonetric.DDD.Domain;

    public class AppSearchResult : IValueObject, IEquatable<AppSearchResult>
	{
		public AppSearchResult (Guid applicationId, string name)
		{
			this.ApplicationId = applicationId;
			this.Name = name;
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

