using System;
using Nancy.ViewEngines;

namespace IdentityWeb
{
	public class MockRenderContext : IRenderContext
	{
		#region IRenderContext implementation
		public string ParsePath (string input)
		{
			throw new NotImplementedException ();
		}
		public string HtmlEncode (string input)
		{
			throw new NotImplementedException ();
		}
		public ViewLocationResult LocateView (string viewName, dynamic model)
		{
			throw new NotImplementedException ();
		}
		public System.Collections.Generic.KeyValuePair<string, string> GetCsrfToken ()
		{
			throw new NotImplementedException ();
		}
		public Nancy.NancyContext Context {
			get {
				throw new NotImplementedException ();
			}
		}
		public IViewCache ViewCache {
			get {
				throw new NotImplementedException ();
			}
		}
		public Nancy.Localization.ITextResource TextResource {
			get {
				throw new NotImplementedException ();
			}
		}
		public dynamic TextResourceFinder {
			get {
				throw new NotImplementedException ();
			}
		}
		#endregion
	}
}

