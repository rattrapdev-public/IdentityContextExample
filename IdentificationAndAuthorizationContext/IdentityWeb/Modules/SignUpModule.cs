namespace IdentityWeb.Modules
{
    using System;
    using System.Dynamic;

    using Geonetric.Identity.Application;
    using Geonetric.Identity.Domain.Clients;

    using Nancy;
    using Nancy.ModelBinding;

    public class SignUpModule : NancyModule
	{
		public SignUpModule (IClientService clientService) : base("/SignUp")
		{
			this.Get [""] = parameters => 
			{
				return this.View["Views/SignUp"];
			};
			this.Post [""] = parameters => 
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
					return this.View["Views/SignUp", errorResponse];
				}
				catch(DuplicateContactEmailException) 
				{
					dynamic errorResponse = new ExpandoObject();
					errorResponse.errorMessage = "The contact email is already assigned to another client!";
					errorResponse.clientName = clientViewModel.ClientName;
					errorResponse.contactName = clientViewModel.ContactName;
					errorResponse.contactPhone = clientViewModel.ContactPhone;
					errorResponse.contactEmail = string.Empty;
					return this.View["Views/SignUp", errorResponse];
				}
				catch(ArgumentException) 
				{
					dynamic errorResponse = new ExpandoObject();
					errorResponse.errorMessage = "The client name, contact name, and contact phone must be present when signing up!";
					errorResponse.clientName = clientViewModel.ClientName;
					errorResponse.contactName = clientViewModel.ContactName;
					errorResponse.contactPhone = clientViewModel.ContactPhone;
					errorResponse.contactEmail = clientViewModel.ContactEmail;
					return this.View["Views/SignUp", errorResponse];
				}

				return this.Response.AsRedirect("/SignUp/ThankYou");
			};
			this.Get ["/ThankYou"] = parameters => 
			{
				return this.View["Views/SignUpThankYou"];
			};
		}
	}
}

