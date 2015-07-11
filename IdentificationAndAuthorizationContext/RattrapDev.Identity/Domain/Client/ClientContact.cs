using System;
using RattrapDev.DDD;

namespace RattrapDev.Identity.Domain.Client
{
	public class ClientContact : IValueObject, IEquatable<ClientContact>
	{
		private string name;

		private string phone;

		public ClientContact (string contactName, string contactPhone)
		{
			this.Name = contactName;
			this.Phone = contactPhone;
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
					throw new ArgumentException ("The client contact name cannot be null or empty!");
				}

				name = value;
			}
		}

		public string Phone 
		{
			get {
				return phone;
			}
			private set {
				if (string.IsNullOrWhiteSpace (value)) 
				{
					throw new ArgumentException ("The client contact phone cannot be null or empty!");
				}

				phone = value;
			}
		}

		#region IEquatable implementation

		public bool Equals (ClientContact other)
		{
			if (other == null)
				return false;
			return other.Name.Equals (other.Name)
			&& other.Phone.Equals (other.Phone);
		}

		#endregion
	}
}

