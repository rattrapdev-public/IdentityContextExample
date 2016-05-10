namespace IdentityWeb.Modules.Test
{
    using System;

    using Nancy;

    public class ErrorModule : NancyModule
	{
		public ErrorModule ()
			:base("error")
		{
			this.Get [""] = _ => {
				throw new Exception ("There was a problem!");
			};
		}
	}
}

