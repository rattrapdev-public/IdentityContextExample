using System;
using RattrapDev.DDD;

namespace RattrapDev.Identity.Domain.Users
{
	public class LoginInfo : IValueObject, IEquatable<LoginInfo>
	{
		private string username;
		private string password;

		public LoginInfo (string username, string password)
		{
			Username = username;
			Password = password;
		}

		public string Username 
		{
			get 
			{
				return username;
			}
			private set 
			{
				if (string.IsNullOrWhiteSpace (value)) 
				{
					throw new ArgumentException ("The username is required!");
				}

				username = value;
			}
		}

		public string Password 
		{
			get 
			{
				return password;
			}
			private set 
			{
				if (string.IsNullOrWhiteSpace (value)) 
				{
					throw new ArgumentException ("The password is required!");
				}

				password = value;
			}
		}

		public bool ValidatePassword(string password) 
		{
			return Password.Equals (password);
		}

		public bool Equals (LoginInfo other)
		{
			if (other == null) 
			{
				return false;
			}

			if (ReferenceEquals (this, other)) 
			{
				return true;
			}

			return Username.Equals (other.Username) && Password.Equals(other.Password);
		}
	}
}

