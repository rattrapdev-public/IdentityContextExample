using System;
using RattrapDev.Identity.Domain.Client;
using System.Dynamic;

namespace RattrapDev.Identity
{
	public static class ClientPresentationObjectFactory
	{
		public static dynamic CreatePresentationObjectFrom(Client client) 
		{
			dynamic presentationObject = new ExpandoObject ();
			presentationObject.Identity = client.Identifier.Identity;
			presentationObject.ClientName = client.ClientDetails.Name;
			presentationObject.Status = client.ClientDetails.Status.ToString();
			presentationObject.ContactName = client.ContactInfo.Name;
			presentationObject.ContactPhone = client.ContactInfo.Phone;

			return presentationObject;
		}

		public static dynamic CreateEmptyPresentationObject() 
		{
			dynamic presentationObject = new ExpandoObject();
			presentationObject.Identity = string.Empty;
			presentationObject.ClientName = string.Empty;
			presentationObject.Status = string.Empty;
			presentationObject.ContactName = string.Empty;
			presentationObject.ContactPhone = string.Empty;

			return presentationObject;
		}
	}
}

