using System;
using RattrapDev.DDD;
using System.Net.Mail;

namespace RattrapDev.Identity.Domain.Users
{
	public class Email : IValueObject
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
	}
}

