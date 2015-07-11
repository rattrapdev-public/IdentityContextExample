using System;
using Nancy;

namespace IdentityWeb
{
	public class UserAdminModule : NancyModule
	{
		public UserAdminModule () : base("/admin/users")
		{
			Get ["/"] = parameters => 
			{
				return View["Views/Admin/UserAdminSearch"];
			};
		}
	}
}

