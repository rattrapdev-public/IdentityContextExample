using System;

namespace RattrapDev.Identity.Domain.Application
{
	public class ApplicationUrl
	{
		private string baseUrl;

		public ApplicationUrl (string baseUrl)
		{
			BaseUrl = baseUrl;
		}

		public string BaseUrl 
		{
			get 
			{
				return baseUrl;
			}
			private set 
			{
				baseUrl = new Uri (value).GetLeftPart(System.UriPartial.Authority);
			}
		}
	}
}

