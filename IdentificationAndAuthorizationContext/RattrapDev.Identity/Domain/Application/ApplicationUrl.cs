using System;
using RattrapDev.DDD;

namespace RattrapDev.Identity.Domain.Application
{
	public class ApplicationUrl : IValueObject, IEquatable<ApplicationUrl>
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

		public bool Equals (ApplicationUrl other)
		{
			if (other == null) 
			{
				return false;
			}
			if (ReferenceEquals (this, other))
				return true;
			return BaseUrl.Equals (other.BaseUrl);
		}
	}
}

