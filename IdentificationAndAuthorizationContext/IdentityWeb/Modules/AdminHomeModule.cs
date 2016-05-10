namespace IdentityWeb.Modules
{
    using Nancy;
    using Nancy.Security;

    public class AdminHomeModule : NancyModule
	{
		public AdminHomeModule ()
			: base("/admin")
		{
			this.RequiresAuthentication ();

			this.Get ["/"] = parameters => 
			{
				return this.View["Views/Admin/AdminHome"];
			};
		}
	}
}

