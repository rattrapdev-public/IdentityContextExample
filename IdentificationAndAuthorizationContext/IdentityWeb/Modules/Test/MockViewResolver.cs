namespace IdentityWeb.Modules.Test
{
    using System;
    using System.IO;

    using Nancy.ViewEngines;

    public class MockViewResolver : IViewResolver
	{
		public ViewLocationResult GetViewLocation (string viewName, dynamic model, ViewLocationContext viewLocationContext)
		{
			Func<TextReader> method = delegate()
			{
				return new StringReader("<p>Hello</p>");
			};

			return new ViewLocationResult(string.Empty, string.Empty, "cshtml", method);
		}
	}
}

