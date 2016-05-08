using System;
using RattrapDev.Identity.Domain.Users;
using System.Collections.Generic;

namespace RattrapDev.Identity.Application
{
	public class UserService : IUserService
	{
		private IUserRepository repository;

		public UserService (IUserRepository repository)
		{
			this.repository = repository;
		}

		public IReadOnlyList<UserSearchResult> GetAllUsers ()
		{
			return repository.All ();
		}

		public UserViewModel GetUser (Guid userId)
		{
			var user = repository.GetByIdentifier (new UserIdentifier ());
			return new UserViewModel {
				UserId = user.Identifier.Id,
				ClientId = user.ClientIdentifier.Identity,
				Username = user.LoginInfo.Username,
				FirstName = user.Name.FirstName,
				LastName = user.Name.LastName,
				Email = user.Email.EmailAddress
			};
		}

		public UserViewModel SaveUser (UserViewModel viewModel)
		{
			User user;
			if (viewModel.UserId.Equals (Guid.Empty)) 
			{
				user = new User (viewModel.ClientId, viewModel.Username, viewModel.Password, viewModel.FirstName, viewModel.LastName, viewModel.Email);
			} 
			else 
			{
				user = this.repository.GetByIdentifier (new UserIdentifier (viewModel.UserId));
				user.UpdateDemographicInfo (viewModel.FirstName, viewModel.LastName, viewModel.Email);
			}

			repository.Store (user);

			return new UserViewModel 
			{
				UserId = user.Identifier.Id,
				ClientId = user.ClientIdentifier.Identity,
				Username = user.LoginInfo.Username,
				FirstName = user.Name.FirstName,
				LastName = user.Name.LastName,
				Email = user.Email.EmailAddress
			};
		}

		public void ResetPassword (ResetPasswordViewModel viewModel)
		{
			var user = repository.GetByIdentifier (new UserIdentifier (viewModel.UserId));
			user.ResetPassword (viewModel.CurrentPassword, viewModel.NewPassword);
			repository.Store (user);
		}
	}
}

