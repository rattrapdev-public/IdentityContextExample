namespace IdentityWeb.Modules.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Geonetric.Identity.Application;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Validation;

    public class ClientAdminModule : NancyModule
	{
		public ClientAdminModule(IClientService clientService) : base("/admin/clients") 
		{
			this.Get ["/"] = parameters => 
			{
				var clientList = clientService.GetAll();
				return this.View["Views/Admin/ClientAdminSearch", clientList];
			};

			this.Get ["/{ClientIdentity}"] = parameters => 
			{
				Guid clientIdentity;
				if (!(Guid.TryParse(parameters.ClientIdentity, out clientIdentity))) 
				{
					throw new ArgumentException("The Client Identity must be a Guid");
				}

				string result = this.Request.Query.result.HasValue ? this.Request.Query.result : string.Empty;
				ClientResult clientResult;
				if (Enum.TryParse<ClientResult>(result, out clientResult)) 
				{
					this.ViewBag.ValidationMessage = ClientMessageService.GetValidationMessage(clientResult);
				}

				var client = clientService.GetClient(clientIdentity);

				return this.View["Views/Admin/ClientAdminDetail", client];
			};

			this.Get ["/new"] = parameters => 
			{
				var emptyClient = new ClientViewModel { ClientName = string.Empty, ContactName = string.Empty, ContactPhone = string.Empty, ContactEmail = string.Empty, Status = string.Empty };
				return this.View["Views/Admin/ClientAdminDetail", emptyClient];
			};

			this.Post ["/activate"] = parameters => 
			{
				Guid clientIdentity;
				if (!(Guid.TryParse(this.Request.Form.clientIdentity, out clientIdentity))) 
				{
					throw new ArgumentException("The Client Identity must be a Guid");
				}

				var client = clientService.ActivateClient(clientIdentity);

				return this.Response.AsRedirect ("~/admin/clients/" + client.ClientIdentity + "?result=" + ClientResult.ActivateClient.ToString ());
			};

			this.Post ["/"] = parameters => 
			{
				var viewModel = this.Bind<ClientViewModel>();

				ClientViewModel client;
				ClientResult result;

				var validationResult = this.Validate(viewModel);
				var errorMessages = new List<string>();
				foreach (var errorResult in validationResult.Errors.Values) {
					errorMessages.AddRange(errorResult.Select(e => e.ErrorMessage));
				}

				this.ViewBag.ErrorMessages = string.Join("|", errorMessages);

				if (!validationResult.IsValid) 
				{
					return this.View["Views/Admin/ClientAdminDetail", viewModel];
				}

				if (viewModel.ClientIdentity.Equals(Guid.Empty))
				{
					client = clientService.SaveNewClient(viewModel);
					result = ClientResult.SaveNewClient;
				}
				else 
				{
					client = clientService.UpdateClient(viewModel);
					result = ClientResult.SaveExistingClient;
				}
				return Nancy.FormatterExtensions.AsRedirect(this.Response, "~/admin/clients/" + client.ClientIdentity + "?result=" + result.ToString());
			};
		}
	}
}

