using System;
using RattrapDev.DDD;

namespace RattrapDev.Identity.Domain.Client
{
	public class ClientIdentifier : IValueObject, IEquatable<ClientIdentifier>
	{
		private Guid identity;

		public ClientIdentifier() 
		{
			this.Identity = Guid.NewGuid ();
		}

		public ClientIdentifier (Guid clientIdentity)
		{
			this.Identity = clientIdentity;
		}

		public Guid Identity 
		{
			get 
			{
				return identity;
			}
			private set 
			{
				if (value.Equals(Guid.Empty))
				{
					throw new ArgumentException ("The client identity cannot be an empty Guid!");
				}

				this.identity = value;
			}
		}

		#region IEquatable implementation

		public bool Equals (ClientIdentifier other)
		{
			if (other == null)
				return false;
			return this.Identity.Equals (other.Identity);
		}

		#endregion

		public override int GetHashCode ()
		{
			return identity.GetHashCode ();
		}
	}
}

