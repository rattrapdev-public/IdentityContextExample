namespace Geonetric.Identity.Tests.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Geonetric.Identity.Application;
    using Geonetric.Identity.Domain.Users;
    using Geonetric.Identity.Infrastructure.Users;

    using NSubstitute;

    using NUnit.Framework;

    using Shouldly;

    [TestFixture]
	public class UserServiceTests
	{
		[Test]
		public void GetAllUsers_returns_all_users() 
		{
			IUserRepository repository = Substitute.For<IUserRepository> ();
			var user1 = new User (Guid.NewGuid (), "johndoe", "password", "John", "Doe", "john.doe@email.com");
			var user2 = new User (Guid.NewGuid (), "janedoe", "password", "Jane", "Doe", "jane.doe@email.com");
			IReadOnlyList<UserSearchResult> userList = new List<UserSearchResult> 
											{
												new UserSearchResult(user1.Identifier.Id, Guid.NewGuid(), "Client Name", user1.LoginInfo.Username, user1.Name.FirstName, user1.Name.LastName, user1.Email.EmailAddress),
												new UserSearchResult(user2.Identifier.Id, Guid.NewGuid(), "Client Name", user2.LoginInfo.Username, user2.Name.FirstName, user2.Name.LastName, user2.Email.EmailAddress)
											};
			repository.All ().Returns (userList);

			var service = new UserService (repository);

			var reconstitutedList = service.GetAllUsers ();

			reconstitutedList.Count.ShouldBe (2);
			reconstitutedList.Any (u => u.UserId.Equals(userList[0].UserId)).ShouldBeTrue();
			reconstitutedList.Any (u => u.UserId.Equals(userList[1].UserId)).ShouldBeTrue();
		}

		[Test]
		public void GetUser_returns_user_from_repository() 
		{			
			IUserRepository repository = Substitute.For<IUserRepository> ();
			var user = new User (Guid.NewGuid (), "johndoe", "password", "John", "Doe", "john.doe@email.com");

			repository.GetByIdentifier (Arg.Any<UserIdentifier>()).Returns (user);

			var service = new UserService (repository);

			var retrievedUser = service.GetUser (Guid.NewGuid ());
			retrievedUser.UserId.ShouldBe (user.Identifier.Id);
		}

		[Test]
		public void SaveUser_stores_new_user_in_repository() 
		{
			var viewModel = new UserViewModel 
			{
				UserId = Guid.Empty,
				ClientId = Guid.NewGuid (),
				Username = "jdoe",
				Password = "password",
				FirstName = "John",
				LastName = "Doe",
				Email = "john@doe.com"
			};

			User user = null;
			var repository = Substitute.For<IUserRepository> ();
			repository.Store(Arg.Do<User>(u => user = u));
			repository.GetByIdentifier (Arg.Any<UserIdentifier> ()).Returns ((User)null);

			var service = new UserService (repository);

			var userViewModel = service.SaveUser (viewModel);
			userViewModel.UserId.ShouldNotBe (viewModel.UserId);
			userViewModel.Email.ShouldBe (viewModel.Email);

			user.ShouldNotBeNull ();
			repository.Received ().Store (Arg.Any<User>());
			repository.DidNotReceive ().GetByIdentifier (Arg.Any<UserIdentifier>());
			user.Identifier.Id.ShouldBe (userViewModel.UserId);
			user.LoginInfo.Username.ShouldBe (viewModel.Username);
			user.LoginInfo.Password.ShouldBe (viewModel.Password);
			user.ClientIdentifier.Identity.ShouldBe (viewModel.ClientId);
			user.Name.FirstName.ShouldBe (viewModel.FirstName);
			user.Name.LastName.ShouldBe (viewModel.LastName);
			user.Email.EmailAddress.ShouldBe (viewModel.Email);
		}

		[Test]
		public void SaveUser_stores_existing_user_in_repository() 
		{
			var viewModel = new UserViewModel 
			{
				UserId = Guid.NewGuid(),
				ClientId = Guid.NewGuid (),
				Username = "jdoe",
				Password = "password",
				FirstName = "John",
				LastName = "Doe",
				Email = "john@doe.com"
			};

			var dto = new UserDto 
			{
				Id = viewModel.UserId,
				ClientId = Guid.NewGuid(),
				Username = "jdoe",
				Password = "password",
				FirstName = "Johnny",
				LastName = "Doseph",
				EmailAddress = "johnny@doseph.com"
			};

			User user = null;
			var repository = Substitute.For<IUserRepository> ();
			repository.Store(Arg.Do<User>(u => user = u));
			repository.GetByIdentifier (Arg.Any<UserIdentifier> ()).Returns (new User(dto));

			var service = new UserService (repository);

			var userViewModel = service.SaveUser (viewModel);
			userViewModel.UserId.ShouldBe (viewModel.UserId);
			userViewModel.Email.ShouldBe (viewModel.Email);

			user.ShouldNotBeNull ();
			repository.Received ().Store (Arg.Any<User>());
			repository.Received ().GetByIdentifier (Arg.Any<UserIdentifier>());
			user.Identifier.Id.ShouldBe (userViewModel.UserId);
			user.Name.FirstName.ShouldBe ("John");
			user.Name.LastName.ShouldBe ("Doe");
			user.Email.EmailAddress.ShouldBe ("john@doe.com");
			user.LoginInfo.Username.ShouldBe("jdoe");
		}

		public void ResetPassword_resets_password_and_stores_user() 
		{

			var dto = new UserDto 
			{
				Id = Guid.NewGuid(),
				ClientId = Guid.NewGuid(),
				Username = "jdoe",
				Password = "password",
				FirstName = "Johnny",
				LastName = "Doseph",
				EmailAddress = "johnny@doseph.com"
			};

			User user = null;
			var repository = Substitute.For<IUserRepository> ();
			repository.Store(Arg.Do<User>(u => user = u));
			repository.GetByIdentifier (Arg.Any<UserIdentifier> ()).Returns (new User(dto));

			var service = new UserService (repository);

			var viewModel = new ResetPasswordViewModel 
							{
								UserId = dto.Id,
								NewPassword = "newpassword"
							};

			service.ResetPassword (viewModel);

			user.ShouldNotBeNull ();
			repository.Received ().Store (Arg.Any<User>());
			repository.Received ().GetByIdentifier (Arg.Any<UserIdentifier>());
			user.LoginInfo.ValidatePassword ("newpassword").ShouldBeTrue ();
		}
	}
}

