namespace Geonetric.Identity.Application
{
    using System;
    using System.Collections.Generic;

    using Geonetric.Identity.Domain.Users;

    public class UserService : IUserService
	{
		private IUserRepository repository;

		public UserService (IUserRepository repository)
		{
			this.repository = repository;
		}

		public IReadOnlyList<UserSearchResult> GetAllUsers ()
		{
			return this.repository.All ();
		}

		public UserViewModel GetUser (Guid userId)
		{
			var user = this.repository.GetByIdentifier (new UserIdentifier (userId));
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

			this.repository.Store (user);

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
			var user = this.repository.GetByIdentifier (new UserIdentifier (viewModel.UserId));
			user.ResetPassword (viewModel.NewPassword);
			this.repository.Store (user);
		}
	}
}

