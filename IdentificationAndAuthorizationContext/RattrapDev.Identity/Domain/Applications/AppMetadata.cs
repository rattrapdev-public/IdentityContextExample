using System;
using RattrapDev.DDD;

namespace RattrapDev.Identity.Domain.Applications
{
	public class AppMetadata : IValueObject, IEquatable<AppMetadata>
	{
		private string name;

		public AppMetadata (string name, string description)
		{
			Name = name;
			Description = description;
		}

		public string Name 
		{
			get 
			{
				return name;
			}
			private set 
			{
				if (string.IsNullOrWhiteSpace (value)) 
				{
					throw new ArgumentException ("ApplicationMetadata.Name : A valid Application Name is required!");
				}

				name = value;
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
			return Name.Equals (other.Name) && ((Description == null && other.Description == null) || Description.Equals (other.Description));
		}
	}
}

