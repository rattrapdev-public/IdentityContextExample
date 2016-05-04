using System;
using RattrapDev.DDD;

namespace RattrapDev.Identity
{
	public class UserSearchResult : IValueObject
	{
		public UserSearchResult (Guid userId, string username, string firstName, string lastName, string email)
		{
			UserId = userId;
			Username = username;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
		}

		public Guid UserId { get; private set; }
		public string Username { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public string Email { get; private set; }
	}
}

