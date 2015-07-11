using System;
using Nancy;
using Nancy.Security;

namespace IdentityWeb
{
	public class AdminHomeModule : NancyModule
	{
		public AdminHomeModule ()
			: base("/admin")
		{
			this.RequiresAuthentication ();

			Get ["/"] = parameters => 
			{
				return View["Views/Admin/AdminHome"];
			};
		}
	}
}

