namespace Geonetric.Identity.Domain.Users
{
    using System;

    using Geonetric.DDD.Domain;

    public class UserSearchResult : IValueObject
	{
		public UserSearchResult (Guid userId, Guid clientId, string clientName, string username, string firstName, string lastName, string email)
		{
			this.UserId = userId;
			this.ClientId = clientId;
			this.ClientName = clientName;
			this.Username = username;
			this.FirstName = firstName;
			this.LastName = lastName;
			this.Email = email;
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

