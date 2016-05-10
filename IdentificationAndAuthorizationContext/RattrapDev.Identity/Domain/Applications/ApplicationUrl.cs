namespace Geonetric.Identity.Domain.Applications
{
    using System;

    using Geonetric.DDD.Domain;

    public class ApplicationUrl : IValueObject, IEquatable<ApplicationUrl>
	{
		private string baseUrl;

		public ApplicationUrl (string baseUrl)
		{
			this.BaseUrl = baseUrl;
		}

		public string BaseUrl 
		{
			get 
			{
				return this.baseUrl;
			}
			private set 
			{
				this.baseUrl = new Uri (value).GetLeftPart(System.UriPartial.Authority);
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
			return this.BaseUrl.Equals (other.BaseUrl);
		}
	}
}

