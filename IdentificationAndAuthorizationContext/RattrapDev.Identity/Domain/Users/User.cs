namespace Geonetric.Identity.Domain.Users
{
    using System;

    using Geonetric.DDD.Domain;
    using Geonetric.Identity.Domain.Clients;
    using Geonetric.Identity.Infrastructure.Users;

    public class User : IAggregate
	{
		private UserIdentifier identifier;
		private ClientIdentifier clientIdentifier;
		private LoginInfo loginInfo;
		private Name name;
		private Email email;

		public User (Guid clientId, string username, string password, string firstName, string lastName, string email)
		{
			this.Identifier = new UserIdentifier ();
			this.ClientIdentifier = new ClientIdentifier (clientId);
			this.LoginInfo = new LoginInfo (username, password);
			this.Name = new Name (firstName, lastName);
			this.Email = new Email (email);
		}

		public User(UserDto dto) 
		{
			this.Identifier = new UserIdentifier (dto.Id);
			this.ClientIdentifier = new ClientIdentifier (dto.ClientId);
			this.LoginInfo = new LoginInfo (dto.Username, dto.Password);
			this.Name = new Name (dto.FirstName, dto.LastName);
			this.Email = new Email (dto.EmailAddress);
		}

		public UserIdentifier Identifier 
		{
			get 
			{
				return this.identifier;
			}
			private set 
			{
				if (value == null) 
				{
					throw new ArgumentNullException ("value", "User Identifier : Cannot be null!");
				}

				this.identifier = value;
			}
		}

		public ClientIdentifier ClientIdentifier 
		{
			get 
			{
				return this.clientIdentifier;
			}
			private set 
			{
				if (value == null) 
				{
					throw new ArgumentNullException ("value", "Client Identifier : Cannot be null!");
				}

				this.clientIdentifier = value;
			}
		}

		public LoginInfo LoginInfo 
		{
			get 
			{
				return this.loginInfo;
			}
			private set 
			{
				if (value == null) 
				{
					throw new ArgumentNullException ("value", "Login Info : Cannot be null!");
				}

				this.loginInfo = value;
			}
		}

		public Name Name 
		{
			get 
			{
				return this.name;
			}
			private set 
			{
				if (value == null) 
				{
					throw new ArgumentNullException ("value", "Name : Cannot be null!");
				}

				this.name = value;
			}
		}

		public Email Email 
		{
			get 
			{
				return this.email;
			}
			private set 
			{
				if (value == null) 
				{
					throw new ArgumentNullException ("value", "Email : Cannot be null!");
				}

				this.email = value;
			}
		}

		public void ResetPassword(string currentPassword, string newPassword) 
		{
			if (!this.LoginInfo.ValidatePassword (currentPassword)) 
			{
				throw new InvalidatedPasswordException ();
			}

			this.LoginInfo = new LoginInfo (this.LoginInfo.Username, newPassword);
		}

		public void ResetPassword(string newPassword) 
		{
			this.LoginInfo = new LoginInfo (this.LoginInfo.Username, newPassword);
		}

		public void UpdateDemographicInfo(string firstName, string lastName, string email) 
		{
			this.Name = new Name (firstName, lastName);
			this.Email = new Email (email);
		}
	}
}

