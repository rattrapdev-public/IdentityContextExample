using System;
using RattrapDev.DDD;

namespace RattrapDev.Identity.Domain.Users
{
	public class UserSearchResult : IValueObject
	{
		public UserSearchResult (Guid userId, Guid clientId, string clientName, string username, string firstName, string lastName, string email)
		{
			UserId = userId;
			ClientId = clientId;
			ClientName = clientName;
			Username = username;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
		}

		public Guid UserId { get; private set; }
		public Guid ClientId { get; private set; }
		public string ClientName { get; private set; }
		public string Username { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public string Email { get; private set; }
	}
}

