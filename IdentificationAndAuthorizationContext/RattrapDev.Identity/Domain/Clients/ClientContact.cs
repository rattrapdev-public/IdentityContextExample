namespace Geonetric.Identity.Domain.Clients
{
    using System;
    using System.Net.Mail;

    using Geonetric.DDD.Domain;

    public class ClientContact : IValueObject, IEquatable<ClientContact>
	{
		private string name;

		private string phone;

		private string email;

		public ClientContact (string contactName, string contactPhone, string email)
		{
			this.Name = contactName;
			this.Phone = contactPhone;
			this.Email = email;
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
					throw new ArgumentException ("The client contact name cannot be null or empty!");
				}

				this.name = value;
			}
		}

		public string Phone 
		{
			get {
				return this.phone;
			}
			private set {
				if (string.IsNullOrWhiteSpace (value)) 
				{
					throw new ArgumentException ("The client contact phone cannot be null or empty!");
				}

				this.phone = value;
			}
		}

		public string Email 
		{
			get 
			{
				return this.email;
			}
			private set 
			{
				if (string.IsNullOrEmpty (value)) 
				{
					throw new ArgumentException ("The email is required!");
				}

				try 
				{
					new MailAddress (value);
				}
				catch (System.FormatException) 
				{
					throw new ArgumentException ("The email is invalid!");
				}

				this.email = value;
			}
		}

		#region IEquatable implementation

		public bool Equals (ClientContact other)
		{
			if (other == null)
				return false;
			return this.Name.Equals (other.Name)
				&& this.Phone.Equals (other.Phone)
				&& this.Email.Equals (other.Email);
		}

		#endregion
	}
}

