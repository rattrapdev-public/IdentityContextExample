using System;
using RattrapDev.Identity.Domain.Client;

namespace RattrapDev.Identity
{
	public class ClientViewModel
	{
		public ClientViewModel(Client client) 
		{
			ClientIdentity = client.Identifier.Identity;
			ClientName = client.ClientDetails.Name;
			Status = client.ClientDetails.Status.ToString ();
			ContactName = client.ContactInfo.Name;
			ContactPhone = client.ContactInfo.Phone;
			ContactEmail = client.ContactInfo.Email;
		}

		public ClientViewModel() { }

		public Guid ClientIdentity { get; set; }
		public string ClientName { get; set; }
		public string ContactName { get; set; }
		public string ContactPhone { get; set; }
		public string ContactEmail { get; set; }
		public string Status { get; set; }
	}
}

