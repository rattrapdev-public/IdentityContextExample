using System;
using NUnit.Framework;
using Nancy.Testing;

namespace IdentityWebTests
{
	[TestFixture]
	public class WelcomePageTests
	{
		[Test]
		public void ShouldDisplaySignUpLink() 
		{
			var bootstrapper = new CustomTestBootstrapper ();
			var browser = new Browser (bootstrapper);

			BrowserResponse response = browser.Get("/", with => { with.HttpRequest(); });   
			string fieldText = response.Body ["div#SignUpWelcome a"].ShouldExistOnce ().And.InnerText;
			Assert.That (fieldText, Is.EqualTo ("Sign Up"));
			string linkText = response.Body ["div#SignUpWelcome a"].ShouldExistOnce ().And.Attributes["href"];
			Assert.That(linkText, Is.EqualTo("/SignUp"));

			response = browser.Get (linkText, with => {
				with.HttpRequest ();
			});

			string signUpTitleText = response.Body ["h2"].ShouldExistOnce ().And.InnerText;
			Assert.That (signUpTitleText, Is.EqualTo ("Sign Up for an Account!"));
		}
	}
}

