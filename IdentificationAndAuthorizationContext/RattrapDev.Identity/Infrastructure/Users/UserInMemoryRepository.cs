namespace Geonetric.Identity.Infrastructure.Users
{
    using System.Collections.Generic;
    using System.Linq;

    using Geonetric.Identity.Domain.Clients;
    using Geonetric.Identity.Domain.Users;

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
			this.userDictionary [user.Identifier] = user;
		}

		public User GetByIdentifier (UserIdentifier identifier)
		{
			if (this.userDictionary.ContainsKey (identifier)) 
			{
				var user = this.userDictionary [identifier];
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
			var clientList = this.clientRepository.All();
			var userList = new List<UserSearchResult> ();
			foreach (var user in this.userDictionary.Values) 
			{
				var client = clientList.First (c => c.Identifier.Equals (user.ClientIdentifier));
				userList.Add(new UserSearchResult(user.Identifier.Id, client.Identifier.Identity, client.ClientDetails.Name, user.LoginInfo.Username, user.Name.FirstName, user.Name.LastName, user.Email.EmailAddress));
			}

			return userList;
		}
	}
}

