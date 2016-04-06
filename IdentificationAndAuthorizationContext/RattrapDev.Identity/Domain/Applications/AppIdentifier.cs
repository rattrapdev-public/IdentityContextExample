using System;
using RattrapDev.DDD;

namespace RattrapDev.Identity.Domain.Applications
{
	public class AppIdentifier : IValueObject, IEquatable<AppIdentifier>
	{
		private Guid id;

		public AppIdentifier() 
		{
			Id = Guid.NewGuid ();
		}

		public AppIdentifier(Guid applicationId) 
		{
			Id = applicationId;
		}

		public Guid Id 
		{
			get 
			{
				return id;
			}
			private set 
			{
				if (value.Equals(Guid.Empty) )
				{
					throw new ArgumentException ("ApplicationIdentifier.Id : The application id cannot be empty");
				}

				id = value;
			}
		}

		public bool Equals (AppIdentifier other)
		{
			if (other == null) 
			{
				return false;
			}
			if (ReferenceEquals (this, other))
				return true;
			return Id.Equals (other.Id);
		}
	}
}

