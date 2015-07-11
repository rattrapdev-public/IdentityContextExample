using System;
using Nancy;

namespace IdentityWeb
{
	public class CustomRootPathProvider : IRootPathProvider
	{
		public string GetRootPath ()
		{
			return "/Users/mwinger/Projects/IdentificationAndAuthorizationContext/IdentityWeb";
		}
	}
}

