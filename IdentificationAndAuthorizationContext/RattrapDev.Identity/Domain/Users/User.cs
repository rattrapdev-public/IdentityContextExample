using System;
using RattrapDev.DDD;
using RattrapDev.Identity.Infrastructure.Users;

namespace RattrapDev.Identity.Domain.Users
{
	public class User : IAggregate
	{
		private UserIdentifier identifier;
		private LoginInfo loginInfo;
		private Name name;
		private Email email;

		public User (string username, string password, string firstName, string lastName, string email)
		{
			Identifier = new UserIdentifier ();
			LoginInfo = new LoginInfo (username, password);
			Name = new Name (firstName, lastName);
			Email = new Email (email);
		}

		public User(UserDto dto) 
		{
			Identifier = new UserIdentifier (dto.Id);
			LoginInfo = new LoginInfo (dto.Username, dto.Password);
			Name = new Name (dto.FirstName, dto.LastName);
			Email = new Email (dto.EmailAddress);
		}

		public UserIdentifier Identifier 
		{
			get 
			{
				return identifier;
			}
			private set 
			{
				if (value == null) 
				{
					throw new ArgumentNullException ("value", "User Identifier : Cannot be null!");
				}

				identifier = value;
			}
		}

		public LoginInfo LoginInfo 
		{
			get 
			{
				return loginInfo;
			}
			private set 
			{
				if (value == null) 
				{
					throw new ArgumentNullException ("value", "Login Info : Cannot be null!");
				}

				loginInfo = value;
			}
		}

		public Name Name 
		{
			get 
			{
				return name;
			}
			private set 
			{
				if (value == null) 
				{
					throw new ArgumentNullException ("value", "Name : Cannot be null!");
				}

				name = value;
			}
		}

		public Email Email 
		{
			get 
			{
				return email;
			}
			private set 
			{
				if (value == null) 
				{
					throw new ArgumentNullException ("value", "Email : Cannot be null!");
				}

				email = value;
			}
		}

		public void ResetPassword(string currentPassword, string newPassword) 
		{
			if (!LoginInfo.ValidatePassword (currentPassword)) 
			{
				throw new InvalidatedPasswordException ();
			}

			LoginInfo = new LoginInfo (LoginInfo.Username, newPassword);
		}

		public void UpdateDemographicInfo(string firstName, string lastName, string email) 
		{
			Name = new Name (firstName, lastName);
			Email = new Email (email);
		}
	}
}

