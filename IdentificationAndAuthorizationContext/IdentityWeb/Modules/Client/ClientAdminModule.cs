using System;
using Nancy;
using RattrapDev.Identity;
using System.Collections.Generic;
using RattrapDev.Identity.Domain.Client;
using Nancy.ModelBinding;

namespace IdentityWeb
{
	public class ClientAdminModule : NancyModule
	{
		public ClientAdminModule(IClientService clientService) : base("/admin/clients") 
		{
			Get ["/"] = parameters => 
			{
				IReadOnlyList<dynamic> clientList = clientService.GetAll();
				return View["Views/Admin/ClientAdminSearch", clientList];
			};

			Get ["/{ClientIdentity}"] = parameters => 
			{
				Guid clientIdentity;
				if (!(Guid.TryParse(parameters.ClientIdentity, out clientIdentity))) 
				{
					throw new ArgumentException("The Client Identity must be a Guid");
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

				return Nancy.FormatterExtensions.AsRedirect(Response, "~/admin/clients/" + client.ClientIdentity);
			};

			Post ["/"] = parameters => 
			{
				var viewModel = this.Bind<ClientViewModel>();

				ClientViewModel client;
				if (string.IsNullOrWhiteSpace(Request.Form["clientIdentity"].Value))
				{
					client = clientService.SaveNewClient(viewModel);
				}
				else 
				{
					Guid clientIdentity;
					if (!(Guid.TryParse(Request.Form["clientIdentity"].Value, out clientIdentity))) 
					{
						throw new ArgumentException("The Client Identity must be a Guid");
					}

					client = clientService.UpdateClient(viewModel);
				}
				return Nancy.FormatterExtensions.AsRedirect(Response, "~/admin/clients/" + client.ClientIdentity);
			};
		}
	}
}

