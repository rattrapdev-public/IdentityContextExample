namespace Geonetric.Identity.Application
{
    using System;

    using Geonetric.Identity.Domain.Clients;

    public class ClientViewModel
	{
		public ClientViewModel(Client client) 
		{
			this.ClientIdentity = client.Identifier.Identity;
			this.ClientName = client.ClientDetails.Name;
			this.Status = client.ClientDetails.Status.ToString ();
			this.ContactName = client.ContactInfo.Name;
			this.ContactPhone = client.ContactInfo.Phone;
			this.ContactEmail = client.ContactInfo.Email;
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

