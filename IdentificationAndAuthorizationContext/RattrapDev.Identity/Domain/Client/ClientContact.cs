using System;
using RattrapDev.DDD;
using System.Net.Mail;

namespace RattrapDev.Identity.Domain.Client
{
	public class ClientContact : IValueObject, IEquatable<ClientContact>
	{
		private string name;

		private string phone;

		private string email;

		public ClientContact (string contactName, string contactPhone, string email)
		{
			Name = contactName;
			Phone = contactPhone;
			Email = email;
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

		public string Email 
		{
			get 
			{
				return email;
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

				email = value;
			}
		}

		#region IEquatable implementation

		public bool Equals (ClientContact other)
		{
			if (other == null)
				return false;
			return Name.Equals (other.Name)
				&& Phone.Equals (other.Phone)
				&& Email.Equals (other.Email);
		}

		#endregion
	}
}

