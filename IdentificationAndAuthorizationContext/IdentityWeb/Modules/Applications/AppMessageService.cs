namespace IdentityWeb
{
	public static class AppMessageService
	{
		public static string GetValidationMessage(AppResult result) 
		{
			switch (result) 
			{
				case AppResult.SaveExistingApp:
				case AppResult.SaveNewApp:
					return "The app has been saved successfully!";
			}

			return string.Empty;
		}
	}
}

