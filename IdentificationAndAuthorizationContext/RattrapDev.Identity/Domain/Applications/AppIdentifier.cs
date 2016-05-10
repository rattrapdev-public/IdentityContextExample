namespace Geonetric.Identity.Domain.Applications
{
    using System;

    using Geonetric.DDD.Domain;

    public class AppIdentifier : IValueObject, IEquatable<AppIdentifier>
	{
		private Guid id;

		public AppIdentifier() 
		{
			this.Id = Guid.NewGuid ();
		}

		public AppIdentifier(Guid applicationId) 
		{
			this.Id = applicationId;
		}

		public Guid Id 
		{
			get 
			{
				return this.id;
			}
			private set 
			{
				if (value.Equals(Guid.Empty) )
				{
					throw new ArgumentException ("ApplicationIdentifier.Id : The application id cannot be empty");
				}

				this.id = value;
			}
		}

		public override bool Equals (object obj)
		{
			return this.Equals (obj as AppIdentifier);
		}

		public bool Equals (AppIdentifier other)
		{
			if (other == null) 
			{
				return false;
			}
			if (ReferenceEquals (this, other))
				return true;
			return this.Id.Equals (other.Id);
		}

		public override int GetHashCode ()
		{
			return this.Id.GetHashCode ();
		}
	}
}

