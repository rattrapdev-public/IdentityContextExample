using System;
using RattrapDev.DDD;

namespace RattrapDev.Identity.Domain.Application
{
	public class ApplicationIdentifier : IValueObject, IEquatable<ApplicationIdentifier>
	{
		private Guid id;

		public ApplicationIdentifier() 
		{
			Id = Guid.NewGuid ();
		}

		public ApplicationIdentifier(Guid applicationId) 
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

		public bool Equals (ApplicationIdentifier other)
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

