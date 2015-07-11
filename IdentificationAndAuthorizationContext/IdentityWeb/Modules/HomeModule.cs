using System;
using Nancy;

namespace IdentityWeb
{
	public class HomeModule : NancyModule
	{
		public HomeModule () : base("/")
		{
			Get ["/"] = parameters => 
			{
				return View["Views/Welcome"];
			};
		}
	}
}

