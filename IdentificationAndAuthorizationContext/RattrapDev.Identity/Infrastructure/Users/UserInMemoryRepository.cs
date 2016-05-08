using System;
using RattrapDev.Identity.Domain.Users;
using System.Collections.Generic;
using RattrapDev.Identity.Infrastructure.Users;
using System.Linq;
using RattrapDev.Identity.Domain.Clients;

namespace RattrapDev.Identity.Infrastructure
{
	public class UserInMemoryRepository : IUserRepository
	{
		private IClientRepository clientRepository;
		private IDictionary<UserIdentifier, User> userDictionary = new Dictionary<UserIdentifier, User> ();

		public UserInMemoryRepository(IClientRepository clientRepository) 
		{
			this.clientRepository = clientRepository;
		}

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
			var clientList = clientRepository.All();
			var userList = new List<UserSearchResult> ();
			foreach (var user in userDictionary.Values) 
			{
				var client = clientList.First (c => c.Identifier.Equals (user.ClientIdentifier));
				userList.Add(new UserSearchResult(user.Identifier.Id, client.Identifier.Identity, client.ClientDetails.Name, user.LoginInfo.Username, user.Name.FirstName, user.Name.LastName, user.Email.EmailAddress));
			}

			return userList;
		}
	}
}

