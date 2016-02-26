using System;
using Nancy;

namespace IdentityWeb
{
	public class ErrorModule : NancyModule
	{
		public ErrorModule ()
			:base("error")
		{
			Get [""] = _ => {
				throw new Exception ("There was a problem!");
			};
		}
	}
}

