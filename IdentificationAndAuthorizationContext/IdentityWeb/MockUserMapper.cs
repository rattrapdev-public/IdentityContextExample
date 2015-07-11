using System;
using Nancy.Authentication.Forms;
using Nancy.Security;
using System.Collections.Generic;

namespace IdentityWeb
{
	public class MockUserMapper : IUserMapper
	{
		public MockUserMapper ()
		{
		}

		public IUserIdentity GetUserFromIdentifier (Guid identifier, Nancy.NancyContext context)
		{
			return new UserIdentity ();
		}

		private class UserIdentity : IUserIdentity 
		{
			public string UserName {
				get {
					return "admin";
				}
			}
			public System.Collections.Generic.IEnumerable<string> Claims {
				get {
					return new List<string> ();
				}
			}
		}
	}
}

