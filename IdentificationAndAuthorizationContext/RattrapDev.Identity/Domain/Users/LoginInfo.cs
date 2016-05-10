namespace Geonetric.Identity.Domain.Users
{
    using System;

    using Geonetric.DDD.Domain;

    public class LoginInfo : IValueObject, IEquatable<LoginInfo>
	{
		private string username;
		private string password;

		public LoginInfo (string username, string password)
		{
			this.Username = username;
			this.Password = password;
		}

		public string Username 
		{
			get 
			{
				return this.username;
			}
			private set 
			{
				if (string.IsNullOrWhiteSpace (value)) 
				{
					throw new ArgumentException ("The username is required!");
				}

				this.username = value;
			}
		}

		public string Password 
		{
			get 
			{
				return this.password;
			}
			private set 
			{
				if (string.IsNullOrWhiteSpace (value)) 
				{
					throw new ArgumentException ("The password is required!");
				}

				this.password = value;
			}
		}

		public bool ValidatePassword(string password) 
		{
			return this.Password.Equals (password);
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

			return this.Username.Equals (other.Username) && this.Password.Equals(other.Password);
		}
	}
}

