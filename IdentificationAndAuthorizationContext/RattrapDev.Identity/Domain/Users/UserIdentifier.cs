using System;
using RattrapDev.DDD;

namespace RattrapDev.Identity.Domain.Users
{
	public class UserIdentifier : IValueObject, IEquatable<UserIdentifier>
	{
		private Guid id;

		public UserIdentifier() 
		{
			Id = Guid.NewGuid ();
		}

		public UserIdentifier(Guid applicationId) 
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

		public override bool Equals (object obj)
		{
			return this.Equals (obj as UserIdentifier);
		}

		public bool Equals (UserIdentifier other)
		{
			if (other == null) 
			{
				return false;
			}
			if (ReferenceEquals (this, other))
				return true;
			return Id.Equals (other.Id);
		}

		public override int GetHashCode ()
		{
			return this.Id.GetHashCode ();
		}
	}
}

