using System;
using NUnit.Framework;
using RattrapDev.Identity.Domain.Users;
using Shouldly;
using RattrapDev.Identity.Infrastructure.Users;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class UserTests
	{
		[Test]
		public void Constructor_creates_new_user() 
		{
			var clientId = Guid.NewGuid ();
			var user = new User (clientId, "username", "password", "John", "Doe", "john@doe.com");
			user.Identifier.Id.ShouldNotBe (Guid.Empty);
			user.ClientIdentifier.Identity.ShouldBe (clientId);
			user.LoginInfo.Username.ShouldBe ("username");
			user.LoginInfo.ValidatePassword ("password").ShouldBeTrue ();
			user.Name.FirstName.ShouldBe ("John");
			user.Name.LastName.ShouldBe ("Doe");
			user.Email.EmailAddress.ShouldBe ("john@doe.com");
		}

		[Test]
		public void Constructor_dto_reconstitutes_user() 
		{
			var userDto = new UserDto 
			{
				Id = Guid.NewGuid (),
				ClientId = Guid.NewGuid(),
				Username = "username",
				Password = "password",
				FirstName = "John",
				LastName = "Doe",
				EmailAddress = "john.doe@email.com"
			};

			var user = new User (userDto);
			user.Identifier.Id.ShouldBe (userDto.Id);
			user.ClientIdentifier.Identity.ShouldBe (userDto.ClientId);
			user.LoginInfo.Username.ShouldBe (userDto.Username);
			user.LoginInfo.ValidatePassword (userDto.Password).ShouldBeTrue();
			user.Name.FirstName.ShouldBe (userDto.FirstName);
			user.Name.LastName.ShouldBe (userDto.LastName);
			user.Email.EmailAddress.ShouldBe (userDto.EmailAddress);
		}

		[Test]
		public void ResetPassword_resets_password_to_new_specified_password() 
		{
			var user = new User (Guid.NewGuid(), "username", "password", "John", "Doe", "john@doe.com");
			user.ResetPassword ("password", "newPassword");
			user.LoginInfo.ValidatePassword ("newPassword").ShouldBeTrue();
		}

		[Test]
		public void ResetPassword_throws_exception_with_invalid_password() 
		{
			var user = new User (Guid.NewGuid(), "username", "password", "John", "Doe", "john@doe.com");
			Should.Throw<InvalidatedPasswordException>(() => user.ResetPassword ("invalidpassword", "newPassword"));
		}

		[Test]
		public void ResetPassword_resets_password_without_need_of_old_password() 
		{
			var user = new User (Guid.NewGuid(), "username", "password", "John", "Doe", "john@doe.com");
			user.ResetPassword ("newPassword");
			user.LoginInfo.ValidatePassword ("newPassword").ShouldBeTrue();
		}

		[Test]
		public void UpdateDemographicInfo_updates_name_and_email() 
		{
			var user = new User (Guid.NewGuid(), "username", "password", "John", "Doe", "john@doe.com");
			user.UpdateDemographicInfo ("Matt", "Smith", "matt.smith@drwho.com");
			user.Name.FirstName.ShouldBe ("Matt");
			user.Name.LastName.ShouldBe ("Smith");
			user.Email.EmailAddress.ShouldBe ("matt.smith@drwho.com");
		}
	}
}

