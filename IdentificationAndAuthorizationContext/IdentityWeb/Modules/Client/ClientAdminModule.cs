using System;
using Nancy;
using RattrapDev.Identity;
using System.Collections.Generic;
using RattrapDev.Identity.Domain.Client;

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

				dynamic client = clientService.GetClient(clientIdentity);

				return View["Views/Admin/ClientAdminDetail", client];
			};

			Get ["/new"] = parameters => 
			{
				dynamic emptyClient = ClientPresentationObjectFactory.CreateEmptyPresentationObject();
				return View["Views/Admin/ClientAdminDetail", emptyClient];
			};

			Post ["/activate"] = parameters => 
			{
				Guid clientIdentity;
				if (!(Guid.TryParse(Request.Form.clientIdentity, out clientIdentity))) 
				{
					throw new ArgumentException("The Client Identity must be a Guid");
				}

				dynamic client = clientService.ActivateClient(clientIdentity);

				return Nancy.FormatterExtensions.AsRedirect(Response, "~/admin/clients/" + client.Identity);
			};

			Post ["/"] = parameters => 
			{
				var clientName = Request.Form["ClientName"].Value;
				var contactName = Request.Form["ContactName"].Value;
				var contactPhone = Request.Form["ContactPhone"].Value;

				dynamic client;
				if (string.IsNullOrWhiteSpace(Request.Form["clientIdentity"].Value))
				{
					client = clientService.SaveNewClient(clientName, contactName, contactPhone);
				}
				else 
				{
					Guid clientIdentity;
					if (!(Guid.TryParse(Request.Form["clientIdentity"].Value, out clientIdentity))) 
					{
						throw new ArgumentException("The Client Identity must be a Guid");
					}

					client = clientService.UpdateClient(clientIdentity, clientName, contactName, contactPhone);
				}
				return Nancy.FormatterExtensions.AsRedirect(Response, "~/admin/clients/" + client.Identity);
				// return Response.AsRedirect("~/admin/clients/" + client.Identity);
			};
		}
	}
}

