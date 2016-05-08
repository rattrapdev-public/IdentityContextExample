using System;
using System.Collections.Generic;
using RattrapDev.Identity.Domain.Users;

namespace RattrapDev.Identity.Application
{
	public interface IUserService
	{
		IReadOnlyList<UserSearchResult> GetAllUsers();
		UserViewModel GetUser(Guid userId);
		UserViewModel SaveUser(UserViewModel viewModel);
		void ResetPassword(Guid userId, string currentPassword, string newPassword);
	}
}

