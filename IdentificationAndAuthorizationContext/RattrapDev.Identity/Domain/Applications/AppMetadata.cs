namespace Geonetric.Identity.Domain.Applications
{
    using System;

    using Geonetric.DDD.Domain;

    public class AppMetadata : IValueObject, IEquatable<AppMetadata>
	{
		private string name;

		public AppMetadata (string name, string description)
		{
			this.Name = name;
			this.Description = description;
		}

		public string Name 
		{
			get 
			{
				return this.name;
			}
			private set 
			{
				if (string.IsNullOrWhiteSpace (value)) 
				{
					throw new ArgumentException ("ApplicationMetadata.Name : A valid Application Name is required!");
				}

				this.name = value;
			}
		}

		public string Description { get; private set; }

		public bool Equals (AppMetadata other)
		{
			if (other == null) 
			{
				return false;
			}
			if (ReferenceEquals (this, other))
				return true;
			return this.Name.Equals (other.Name) && ((this.Description == null && other.Description == null) || this.Description.Equals (other.Description));
		}
	}
}

