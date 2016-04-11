using System;
using Nancy;
using RattrapDev.Identity;
using System.Collections.Generic;
using Nancy.ModelBinding;
using Nancy.Validation;
using System.Linq;

namespace IdentityWeb
{
	public class ClientAdminModule : NancyModule
	{
		public ClientAdminModule(IClientService clientService) : base("/admin/clients") 
		{
			Get ["/"] = parameters => 
			{
				var clientList = clientService.GetAll();
				return View["Views/Admin/ClientAdminSearch", clientList];
			};

			Get ["/{ClientIdentity}"] = parameters => 
			{
				Guid clientIdentity;
				if (!(Guid.TryParse(parameters.ClientIdentity, out clientIdentity))) 
				{
					throw new ArgumentException("The Client Identity must be a Guid");
				}

				string result = Request.Query.result.HasValue ? Request.Query.result : string.Empty;
				ClientResult clientResult;
				if (Enum.TryParse<ClientResult>(result, out clientResult)) 
				{
					ViewBag.ValidationMessage = ClientMessageService.GetValidationMessage(clientResult);
				}

				var client = clientService.GetClient(clientIdentity);

				return View["Views/Admin/ClientAdminDetail", client];
			};

			Get ["/new"] = parameters => 
			{
				var emptyClient = new ClientViewModel { ClientName = string.Empty, ContactName = string.Empty, ContactPhone = string.Empty, Status = string.Empty };
				return View["Views/Admin/ClientAdminDetail", emptyClient];
			};

			Post ["/activate"] = parameters => 
			{
				Guid clientIdentity;
				if (!(Guid.TryParse(Request.Form.clientIdentity, out clientIdentity))) 
				{
					throw new ArgumentException("The Client Identity must be a Guid");
				}

				var client = clientService.ActivateClient(clientIdentity);

				return Response.AsRedirect ("~/admin/clients/" + client.ClientIdentity + "?result=" + ClientResult.ActivateClient.ToString ());
			};

			Post ["/"] = parameters => 
			{
				var viewModel = this.Bind<ClientViewModel>();

				ClientViewModel client;
				ClientResult result;

				var validationResult = this.Validate(viewModel);
				var errorMessages = new List<string>();
				foreach (var errorResult in validationResult.Errors.Values) {
					errorMessages.AddRange(errorResult.Select(e => e.ErrorMessage));
				}

				ViewBag.ErrorMessages = string.Join("|", errorMessages);

				if (!validationResult.IsValid) 
				{
					return View["Views/Admin/ClientAdminDetail", viewModel];
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
				return Nancy.FormatterExtensions.AsRedirect(Response, "~/admin/clients/" + client.ClientIdentity + "?result=" + result.ToString());
			};
		}
	}
}

