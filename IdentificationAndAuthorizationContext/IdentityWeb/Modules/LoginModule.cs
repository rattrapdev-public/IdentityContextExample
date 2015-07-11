using System;
using Nancy;
using Nancy.Authentication.Forms;

namespace IdentityWeb
{
	public class LoginModule : NancyModule
	{
		public LoginModule ()
		{
			Get ["/login"] = parameters => 
			{
				return View["Views/Login"];
			};
			Post ["/login"] = parameters => 
			{
				var username = Request.Form["Username"].Value;
				var password = Request.Form["Password"].Value;

				if (!(username.Equals("admin") && password.Equals("password")) )
				{
					return Response.AsRedirect("/login");
				}

				return this.LoginAndRedirect(Guid.NewGuid(), fallbackRedirectUrl: "/admin");
			};
			Get ["/logout"] = parameters => 
			{
				return this.LogoutAndRedirect("/");
			};
		}
	}
}

