using System;
using Nancy;
using RattrapDev.Identity;
using System.Dynamic;
using Nancy.ModelBinding;

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
				var clientViewModel = this.Bind<ClientViewModel>();

				try 
				{
					clientService.SaveNewClient(clientViewModel);
				}
				catch(DuplicateClientException) 
				{
					dynamic errorResponse = new ExpandoObject();
					errorResponse.errorMessage = "The client name is already assigned to another client!";
					errorResponse.contactName = clientViewModel.ContactName;
					errorResponse.contactPhone = clientViewModel.ContactPhone;
					errorResponse.contactEmail = clientViewModel.ContactEmail;
					return View["Views/SignUp", errorResponse];
				}
				catch(ArgumentException) 
				{
					dynamic errorResponse = new ExpandoObject();
					errorResponse.errorMessage = "The client name, contact name, and contact phone must be present when signing up!";
					errorResponse.clientName = clientViewModel.ClientName;
					errorResponse.contactName = clientViewModel.ContactName;
					errorResponse.contactPhone = clientViewModel.ContactPhone;
					errorResponse.contactEmail = clientViewModel.ContactEmail;
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

