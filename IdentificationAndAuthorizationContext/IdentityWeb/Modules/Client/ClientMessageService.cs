using System;

namespace IdentityWeb
{
	public static class ClientMessageService
	{
		public static string GetValidationMessage(ClientResult result) 
		{
			switch (result) 
			{
				case ClientResult.ActivateClient: 
					return "The client has been activated successfully!";
				case ClientResult.DeactivateClient:
					return "The client has been deactivated successfully!";
				case ClientResult.SaveExistingClient: 
				case ClientResult.SaveNewClient:
					return "The client has been saved successfully!";
				
			}

			return string.Empty;
		}
	}
}

