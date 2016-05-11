using System;
using RattrapDev.DDD;
using System.Net.Mail;

namespace RattrapDev.Identity.Domain.Users
{
	public class Email : IValueObject, IEquatable<Email>
	{
		private string emailAddress;

		public Email (string emailAddress)
		{
			EmailAddress = emailAddress;
		}

		public string EmailAddress 
		{
			get 
			{
				return emailAddress;
			}
			set 
			{
				if (string.IsNullOrWhiteSpace (value)) 
				{
					throw new ArgumentException ("Email : Is Invalid!");
				}

				new MailAddress (value);

				emailAddress = value;
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

			return EmailAddress.Equals (other.EmailAddress);
		}
	}
}

