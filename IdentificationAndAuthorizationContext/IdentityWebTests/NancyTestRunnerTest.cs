using System;
using IdentityWeb;
using Nancy.Testing;
using Nancy;
using NUnit.Framework;

namespace IdentityWebTests
{
	[TestFixture]
	public class NancyTestRunnerTest
	{
		[Test]
		public void TestBrowserReturnsSuccess() 
		{
			var bootstrapper = new CustomTestBootstrapper ();
			var browser = new Browser (bootstrapper);

			BrowserResponse response = browser.Get("/", with => { with.HttpRequest(); });    

			Assert.That (HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
		}
	}
}

