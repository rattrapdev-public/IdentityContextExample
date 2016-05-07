using System;
using System.Collections.Generic;

namespace RattrapDev.Identity.Application
{
	public interface IUserService
	{
		IReadOnlyList<UserSearchResult> GetAllUsers();
		UserViewModel GetUser(Guid userId);
		UserViewModel SaveUser(UserViewModel viewModel);
	}
}

