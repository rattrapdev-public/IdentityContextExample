using System;
using RattrapDev.Identity.Domain.Users;
using System.Collections.Generic;
using RattrapDev.Identity.Infrastructure.Users;
using System.Linq;

namespace RattrapDev.Identity.Infrastructure
{
	public class UserInMemoryRepository : IUserRepository
	{
		private IDictionary<UserIdentifier, User> userDictionary = new Dictionary<UserIdentifier, User> ();

		public void Store (User user)
		{
			userDictionary [user.Identifier] = user;
		}

		public User GetByIdentifier (UserIdentifier identifier)
		{
			if (userDictionary.ContainsKey (identifier)) 
			{
				var user = userDictionary [identifier];
				var dto = new UserDto 
				{
					Id = user.Identifier.Id,
					ClientId = user.ClientIdentifier.Identity,
					Username = user.LoginInfo.Username,
					Password = user.LoginInfo.Password,
					FirstName = user.Name.FirstName,
					LastName = user.Name.LastName,
					EmailAddress = user.Email.EmailAddress
				};

				return new User (dto);
			}

			return null;
		}

		public IReadOnlyList<UserSearchResult> All ()
		{
			return userDictionary.Values.Select (u => new UserSearchResult(u.Identifier.Id, u.LoginInfo.Username, u.Name.FirstName, u.Name.LastName, u.Email.EmailAddress)).ToList();
		}
	}
}

