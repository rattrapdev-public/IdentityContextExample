using System;
using Nancy.ViewEngines.Razor;

namespace IdentityWeb
{
	public class MockNancyRazorView : NancyRazorViewBase
	{
		#region implemented abstract members of NancyRazorViewBase
		public override void Execute ()
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
}

