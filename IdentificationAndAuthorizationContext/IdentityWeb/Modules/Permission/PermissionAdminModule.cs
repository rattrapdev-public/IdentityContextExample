using System;
using Nancy;

namespace IdentityWeb
{
	public class PermissionAdminModule : NancyModule
	{
		public PermissionAdminModule () : base("/admin/permissions")
		{
			Get ["/"] = parameters => 
			{
				return View["Views/Admin/PermissionAdminSearch"];
			};
		}
	}
}

