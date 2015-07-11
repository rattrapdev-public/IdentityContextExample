using System;
using Nancy.ErrorHandling;

namespace IdentityWeb
{
	public class CustomStatusCodeHandler : IStatusCodeHandler
	{
		#region IStatusCodeHandler implementation
		public bool HandlesStatusCode (Nancy.HttpStatusCode statusCode, Nancy.NancyContext context)
		{
			if (statusCode == Nancy.HttpStatusCode.InternalServerError)
				return true;
			return false;
		}
		public void Handle (Nancy.HttpStatusCode statusCode, Nancy.NancyContext context)
		{
			
		}
		#endregion
	}
}

