﻿using System;

namespace RattrapDev.Identity.Application
{
	public class UserViewModel
	{
		public Guid UserId { get; set; }
		public Guid ClientId { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
	}
}

