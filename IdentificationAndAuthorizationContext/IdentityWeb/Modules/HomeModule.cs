namespace IdentityWeb.Modules
{
    using Nancy;

    public class HomeModule : NancyModule
	{
		public HomeModule () : base("/")
		{
			this.Get ["/"] = parameters => 
			{
				return this.View["Views/Welcome"];
			};
		}
	}
}

