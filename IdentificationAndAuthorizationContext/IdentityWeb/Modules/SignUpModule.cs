using System;
using Nancy;
using RattrapDev.Identity;
using System.Dynamic;

namespace IdentityWeb
{
	public class SignUpModule : NancyModule
	{
		public SignUpModule (IClientService clientService) : base("/SignUp")
		{
			Get [""] = parameters => 
			{
				return View["Views/SignUp"];
			};
			Post [""] = parameters => 
			{
				var clientName = Request.Form["ClientName"].Value;
				var contactName = Request.Form["ContactName"].Value;
				var contactPhone = Request.Form["ContactPhone"].Value;

				try 
				{
					clientService.SaveNewClient(clientName, contactName, contactPhone);
				}
				catch(DuplicateClientException) 
				{
					dynamic errorResponse = new ExpandoObject();
					errorResponse.errorMessage = "The client name is already assigned to another client!";
					errorResponse.contactName = contactName;
					errorResponse.contactPhone = contactPhone;
					return View["Views/SignUp", errorResponse];
				}
				catch(ArgumentException) 
				{
					dynamic errorResponse = new ExpandoObject();
					errorResponse.errorMessage = "The client name, contact name, and contact phone must be present when signing up!";
					errorResponse.clientName = clientName;
					errorResponse.contactName = contactName;
					errorResponse.contactPhone = contactPhone;
					return View["Views/SignUp", errorResponse];
				}

				return Response.AsRedirect("/SignUp/ThankYou");
			};
			Get ["/ThankYou"] = parameters => 
			{
				return View["Views/SignUpThankYou"];
			};
		}
	}
}

