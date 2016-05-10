namespace IdentityWeb.Modules
{
    using System;

    using Nancy;
    using Nancy.Authentication.Forms;

    public class LoginModule : NancyModule
	{
		public LoginModule ()
		{
			this.Get ["/login"] = parameters => 
			{
				return this.View["Views/Login"];
			};
			this.Post ["/login"] = parameters => 
			{
				var username = this.Request.Form["Username"].Value;
				var password = this.Request.Form["Password"].Value;

				if (!(username.Equals("admin") && password.Equals("password")) )
				{
					return this.Response.AsRedirect("/login");
				}

				return this.LoginAndRedirect(Guid.NewGuid(), fallbackRedirectUrl: "/admin");
			};
			this.Get ["/logout"] = parameters => 
			{
				return this.LogoutAndRedirect("/");
			};
		}
	}
}

