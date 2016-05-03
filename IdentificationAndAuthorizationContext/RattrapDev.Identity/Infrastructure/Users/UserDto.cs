using System;

namespace RattrapDev.Identity.Infrastructure.Users
{
	public class UserDto
	{
		public Guid Id { get; set; }
		public Guid ClientId { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAddress { get; set; }
	}
}

