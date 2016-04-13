using System;
using RattrapDev.DDD;

namespace RattrapDev.Identity.Domain.Client
{
	public class Client : IAggregate, IEquatable<Client>
	{
		private ClientIdentifier identifier;
		private ClientDetails clientDetails;
		private ClientContact contactInfo;

		/// <summary>
		/// Initializes a new instance of the <see cref="RattrapDev.Identity.Domain.Client.Client"/> class.
		/// Constructor used for signing up of a new Client.
		/// </summary>
		public Client (string clientName, string contactName, string contactPhone, string contactEmail)
		{
			this.Identifier = new ClientIdentifier ();
			this.ClientDetails = new ClientDetails (clientName, ClientStatus.SignedUp);
			this.ContactInfo = new ClientContact (contactName, contactPhone, contactEmail);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RattrapDev.Identity.Domain.Client.Client"/> class.
		/// Used for reconstituting the Client.
		/// </summary>
		public Client(Guid clientIdentity, string clientName, ClientStatus clientStatus, string contactName, string contactPhone, string contactEmail) 
		{
			this.Identifier = new ClientIdentifier (clientIdentity);
			this.ClientDetails = new ClientDetails (clientName, clientStatus);
			this.ContactInfo = new ClientContact (contactName, contactPhone, contactEmail);
		}

		public ClientIdentifier Identifier 
		{
			get 
			{
				return identifier;
			}
			private set 
			{
				identifier = value;
			}
		}

		public ClientDetails ClientDetails 
		{
			get 
			{
				return clientDetails;
			}
			private set 
			{
				clientDetails = value;
			}
		}

		public ClientContact ContactInfo 
		{
			get 
			{
				return contactInfo;
			}
			set 
			{
				contactInfo = value;
			}
		}

		public void Activate() 
		{
			this.ClientDetails = new ClientDetails (this.ClientDetails.Name, ClientStatus.Online);
		}

		public void UpdateClientContactInfo(string name, string phone, string email) 
		{
			this.ContactInfo = new ClientContact (name, phone, email);
		}

		public void UpdateClientDetails(string name) 
		{
			this.ClientDetails = new ClientDetails (name, this.ClientDetails.Status);
		}

		#region IEquatable implementation

		public bool Equals (Client other)
		{
			if (other == null)
				return false;
			return other.Identifier.Equals (this.Identifier)
				&& other.ClientDetails.Equals (this.ClientDetails)
				&& other.ContactInfo.Equals (this.ContactInfo);
		}

		#endregion
	}
}

