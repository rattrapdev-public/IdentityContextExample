using System;
using Nancy.ErrorHandling;
using Nancy.ViewEngines;

namespace IdentityWeb
{
	public class CustomStatusCodeHandler : DefaultViewRenderer, IStatusCodeHandler
	{
		public CustomStatusCodeHandler(IViewFactory factory) : base(factory)  
		{  
		} 

		#region IStatusCodeHandler implementation
		public bool HandlesStatusCode (Nancy.HttpStatusCode statusCode, Nancy.NancyContext context)
		{
			if (statusCode == Nancy.HttpStatusCode.InternalServerError)
				return false;
			return false;
		}
		public void Handle (Nancy.HttpStatusCode statusCode, Nancy.NancyContext context)
		{
			var response = RenderView(context, "CustomErrorPage");
			response.StatusCode = statusCode;
			context.Response = response;
		}
		#endregion
	}
}

