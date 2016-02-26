using System;
using NUnit.Framework;
using Nancy.Testing;

namespace IdentityWebTests
{
	[TestFixture]
	public class LoginTests
	{
		[Test]
		public void ShouldRedirectToLoginWithSecureRequest() 
		{
			var bootstrapper = new CustomTestBootstrapper ();
			var browser = new Browser (bootstrapper);

			BrowserResponse response = browser.Get("/admin", with => { with.HttpRequest(); });

			response.ShouldHaveRedirectedTo ("/login?returnUrl=/admin");
		}

		[Test]
		public void ShouldAccessLoginAfterSuccessfulLogin() 
		{
			var bootstrapper = new CustomTestBootstrapper ();
			var browser = new Browser (bootstrapper);

			BrowserResponse response = browser.Post ("/login", (with) => {
				with.HttpRequest();
				with.FormValue("Username", "admin");
				with.FormValue("Password", "password");
			});

			response.ShouldHaveRedirectedTo ("/admin");
		}

		[Test]
		public void LoggingInMaintainsSession() 
		{
			var bootstrapper = new CustomTestBootstrapper ();
			var browser = new Browser (bootstrapper);

			BrowserResponse response = browser.Post ("/login", (with) => {
				with.HttpRequest ();
				with.FormValue ("Username", "admin");
				with.FormValue ("Password", "password");
			});

			response = browser.Get ("/admin");
			string innerText = response.Body ["div#site-content h2"].ShouldExistOnce ().And.InnerText;
			Assert.That(innerText, Is.EqualTo("Client Manager Admin Home"));
		} 
	}
}

