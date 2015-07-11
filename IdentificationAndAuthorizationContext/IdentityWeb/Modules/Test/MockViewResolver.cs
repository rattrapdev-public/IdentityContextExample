using System;
using Nancy.ViewEngines;
using System.IO;

namespace IdentityWeb
{
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

