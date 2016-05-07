using System;
using NUnit.Framework;
using RattrapDev.Identity.Domain.Users;
using RattrapDev.Identity.Infrastructure.Users;
using Shouldly;
using System.Linq;
using RattrapDev.Identity.Infrastructure;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class UserInMemoryRepositoryTests
	{
		[Test]
		public void StoreAndGetByIdentifier_stores_and_gets_application() 
		{
			var repository = new UserInMemoryRepository ();
			var user = new User (new UserDto{ Id = Guid.NewGuid(), ClientId = Guid.NewGuid(), Username = "jdoe", Password = "password", FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@email.com" });
			repository.Store (user);
			var reconstitutedUser = repository.GetByIdentifier (user.Identifier);
			user.Identifier.ShouldBe (reconstitutedUser.Identifier);
			user.ClientIdentifier.ShouldBe (reconstitutedUser.ClientIdentifier);
			user.LoginInfo.ShouldBe (reconstitutedUser.LoginInfo);
			user.Name.ShouldBe (reconstitutedUser.Name);
			user.Email.ShouldBe (reconstitutedUser.Email);
		}

		[Test]
		public void GetAll_returns_preexisting_app() 
		{
			var repository = new UserInMemoryRepository ();
			var user1 = new User (new UserDto{ Id = Guid.NewGuid(), ClientId = Guid.NewGuid(), Username = "jdoe", Password = "password", FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@email.com" });
			var user2 = new User (new UserDto{ Id = Guid.NewGuid(), ClientId = Guid.NewGuid(), Username = "jdoe", Password = "password", FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@email.com" });
			var user3 = new User (new UserDto{ Id = Guid.NewGuid(), ClientId = Guid.NewGuid(), Username = "jdoe", Password = "password", FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@email.com" });

			repository.Store (user1);
			repository.Store (user2);
			repository.Store (user3);

			var list = repository.All ();
			list.Any (u => u.UserId.Equals (user1.Identifier.Id)).ShouldBeTrue ();
			list.Any (u => u.UserId.Equals (user2.Identifier.Id)).ShouldBeTrue ();
			list.Any (u => u.UserId.Equals (user3.Identifier.Id)).ShouldBeTrue ();
			list.Count.ShouldBe (3);
		}
	}
}

