using System;
using RattrapDev.DDD;

namespace RattrapDev.Identity
{
	public class ApplicationIdentifier : IValueObject
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
	}
}

