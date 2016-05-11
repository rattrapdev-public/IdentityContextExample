using System;
using System.Collections.Generic;

namespace RattrapDev.Identity.Domain.Users
{
	public interface IUserRepository
	{
		void Store(User user);
		User GetByIdentifier(UserIdentifier identifier);
		IReadOnlyList<UserSearchResult> All();
	}
}

