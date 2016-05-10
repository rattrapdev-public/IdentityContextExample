namespace IdentityWebTests
{
    using Nancy;

    public class CustomRootPathProvider : IRootPathProvider
	{
		public string GetRootPath ()
		{
			return "/Users/mwinger/IdentityContextExample/IdentificationAndAuthorizationContext/IdentityWeb";
		}
	}
}

