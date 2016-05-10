namespace Geonetric.Identity.Application
{
    using System;
    using System.Collections.Generic;

    using Geonetric.Identity.Domain.Users;

    public interface IUserService
	{
		IReadOnlyList<UserSearchResult> GetAllUsers();
		UserViewModel GetUser(Guid userId);
		UserViewModel SaveUser(UserViewModel viewModel);
		void ResetPassword(ResetPasswordViewModel viewModel);
	}
}

