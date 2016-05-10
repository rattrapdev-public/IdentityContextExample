using System;

namespace IdentityWeb
{
	public static class UserMessageService
	{
		public static string GetValidationMessage(UserResult result) 
		{
			switch (result) 
			{
				case UserResult.SaveUser: 
					return "The user has been saved successfully!";
				case UserResult.ResetUserPassword: 
					return "The user's password has been reset successfully!";
			}

			return string.Empty;
		}
	}
}

