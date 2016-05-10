namespace Geonetric.Identity.Tests.Infrastructure.Users
{
    using System;
    using System.Linq;

    using Geonetric.Identity.Domain.Clients;
    using Geonetric.Identity.Domain.Users;
    using Geonetric.Identity.Infrastructure.Client;
    using Geonetric.Identity.Infrastructure.Users;

    using NUnit.Framework;

    using Shouldly;

    [TestFixture]
	public class UserInMemoryRepositoryTests
	{
		[Test]
		public void StoreAndGetByIdentifier_stores_and_gets_application() 
		{
			var repository = new UserInMemoryRepository (new ClientInMemoryRepository());
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
			var clientRepository = new ClientInMemoryRepository ();
			var client = new Client ("Client Name", "John", "1234567890", "john@doe.com");
			clientRepository.Store(client);

			var repository = new UserInMemoryRepository (clientRepository);
			var user1 = new User (new UserDto{ Id = Guid.NewGuid(), ClientId = client.Identifier.Identity, Username = "jdoe", Password = "password", FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@email.com" });
			var user2 = new User (new UserDto{ Id = Guid.NewGuid(), ClientId = client.Identifier.Identity, Username = "jdoe", Password = "password", FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@email.com" });
			var user3 = new User (new UserDto{ Id = Guid.NewGuid(), ClientId = client.Identifier.Identity, Username = "jdoe", Password = "password", FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@email.com" });

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

