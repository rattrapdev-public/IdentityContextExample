using System;
using System.Collections.Generic;
using RattrapDev.Identity.Domain.Client;
using System.Linq;

namespace RattrapDev.Identity.Infrastructure
{
	public class ClientInMemoryRepository : IClientRepository
	{
		private static Dictionary<ClientIdentifier, Client> dict = new Dictionary<ClientIdentifier, Client>();

		public ClientInMemoryRepository() 
		{
			ClearDictionary ();
		}

		#region IClientRepository implementation

		public Client GetBy (ClientIdentifier identifier)
		{
			if (dict.ContainsKey (identifier)) 
			{
				return dict [identifier];
			}

			return null;
		}

		public void Store (Client client)
		{
			var clientToSave = new Client(
				client.Identifier.Identity, 
				client.ClientDetails.Name, 
				client.ClientDetails.Status, 
				client.ContactInfo.Name, 
				client.ContactInfo.Phone,
				client.ContactInfo.Email);
			if (dict.ContainsKey (client.Identifier)) 
			{
				dict [client.Identifier] = clientToSave;
			} 
			else 
			{
				dict.Add (client.Identifier, clientToSave);
			}
		}

		public IReadOnlyList<Client> All ()
		{
			return new List<Client>(dict.Values);
		}

		public static void ClearDictionary() 
		{
			dict = new Dictionary<ClientIdentifier, Client> ();

			var clientToAdd = new Client ("Acme Corporation", "Wile E. Coyote", "3195553434", "wile.coyote@acme.com");
			dict.Add(clientToAdd.Identifier, clientToAdd);
			clientToAdd = new Client ("Geonetric", "John Doe", "3195553221", "john.doe@geonetric.com");
			dict.Add (clientToAdd.Identifier, clientToAdd);
		}

		#endregion
	}
}

