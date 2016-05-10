namespace Geonetric.Identity.Domain.Users
{
    using System.Collections.Generic;

    public interface IUserRepository
	{
		void Store(User user);
		User GetByIdentifier(UserIdentifier identifier);
		IReadOnlyList<UserSearchResult> All();
	}
}

