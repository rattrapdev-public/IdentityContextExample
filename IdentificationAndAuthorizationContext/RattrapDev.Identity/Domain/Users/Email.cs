namespace Geonetric.Identity.Domain.Users
{
    using System;
    using System.Net.Mail;

    using Geonetric.DDD.Domain;

    public class Email : IValueObject, IEquatable<Email>
	{
		private string emailAddress;

		public Email (string emailAddress)
		{
			this.EmailAddress = emailAddress;
		}

		public string EmailAddress 
		{
			get 
			{
				return this.emailAddress;
			}
			set 
			{
				if (string.IsNullOrWhiteSpace (value)) 
				{
					throw new ArgumentException ("Email : Is Invalid!");
				}

				new MailAddress (value);

				this.emailAddress = value;
			}
		}

		public bool Equals (Email other)
		{
			if (other == null) 
			{
				return false;
			}

			if (ReferenceEquals (this, other)) 
			{
				return true;
			}

			return this.EmailAddress.Equals (other.EmailAddress);
		}
	}
}

