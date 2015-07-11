using System;
using NUnit.Framework;
using Nancy.Testing;
using RattrapDev.Identity;
using System.Collections.Generic;
using System.Linq;
using RattrapDev.Identity.Domain.Client;

namespace IdentityWebTests
{
	[TestFixture]
	public class SignUpPageTests
	{
		[Test]
		public void ShouldDisplayFormElements() 
		{
			var bootstrapper = new CustomTestBootstrapper ();
			var browser = new Browser (bootstrapper);

			BrowserResponse response = browser.Get("/SignUp", with => { with.HttpRequest(); });
			response.Body ["form#signUpForm input#ClientName"].ShouldExistOnce ();
			response.Body ["form#signUpForm input#ContactName"].ShouldExistOnce ();
			response.Body ["form#signUpForm input#ContactPhone"].ShouldExistOnce ();
		}

		[Test]
		public void ShouldCreateClientWithNewSignUp() 
		{
			IClientService clientService = new ClientService ();
			var bootstrapper = new CustomTestBootstrapper (clientService);
			var browser = new Browser (bootstrapper);

			BrowserResponse response = browser.Post ("/SignUp", (with) => {
				with.HttpRequest();
				with.FormValue("ClientName", "Unique Client Name");
				with.FormValue("ContactName", "James Dean");
				with.FormValue("ContactPhone", "8008674309");
			});

			response.ShouldHaveRedirectedTo ("/SignUp/ThankYou");

			IReadOnlyList<Client> clients = clientService.GetAll ();
			bool foundClient = false;
			foreach (var client in clients) 
			{
				if (client.ClientDetails.Name.Equals ("Unique Client Name")) 
				{
					foundClient = true;
				}
			}

			Assert.IsTrue (foundClient);
		}

		[Test]
		public void ShouldReturnMessageWithDuplicateClientName() 
		{
			IClientService clientService = new ClientService ();
			var bootstrapper = new CustomTestBootstrapper (clientService);
			var browser = new Browser (bootstrapper);

			IReadOnlyList<Client> clients = clientService.GetAll ();
			var client = clients.First ();

			BrowserResponse response = browser.Post ("/SignUp", (with) => {
				with.HttpRequest();
				with.FormValue("ClientName", client.ClientDetails.Name);
				with.FormValue("ContactName", "James Dean");
				with.FormValue("ContactPhone", "8008674309");
			});

			string validationText = response.Body ["div#ValidationText p"].ShouldExistOnce ().And.InnerText;
			Assert.That(validationText, Is.StringContaining("The client name is already assigned to another client!"));
		}

		[Test]
		public void ShouldShowMessageWithNullInput() 
		{
			IClientService clientService = new ClientService ();
			var bootstrapper = new CustomTestBootstrapper (clientService);
			var browser = new Browser (bootstrapper);

			BrowserResponse response = browser.Post ("/SignUp", (with) => {
				with.HttpRequest();
				with.FormValue("ContactName", "James Dean");
				with.FormValue("ContactPhone", "8008674309");
			});

			string validationText = response.Body ["div#ValidationText p"].ShouldExistOnce ().And.InnerText;
			Assert.That(validationText, Is.StringContaining("The client name, contact name, and contact phone must be present when signing up!"));
		}
	}
}

