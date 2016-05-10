namespace Geonetric.Identity.Domain.Applications
{
    using System;

    using Geonetric.DDD.Domain;
    using Geonetric.Identity.Infrastructure.Applications;

    public class App : IAggregate
	{
		private AppIdentifier identifier;
		private AppMetadata metadata;
		private ApplicationUrl url;

		public App(string name, string description, string url) 
		{
			this.Identifier = new AppIdentifier ();
			this.Metadata = new AppMetadata (name, description);
			this.Url = new ApplicationUrl (url);
		}

		public App(AppDto dto) 
		{
			this.Identifier = new AppIdentifier (dto.Id);
			this.Metadata = new AppMetadata (dto.Name, dto.Description);
			this.Url = new ApplicationUrl (dto.BaseUrl);
		}

		public AppIdentifier Identifier 
		{
			get 
			{
				return this.identifier;
			}
			private set 
			{
				if (value == null) 
				{
					throw new ArgumentNullException ("value", "The identifier cannot be null!");
				}

				this.identifier = value;
			}
		}

		public AppMetadata Metadata 
		{
			get 
			{
				return this.metadata;
			}
			private set 
			{
				if (value == null) 
				{
					throw new ArgumentNullException ("value", "The application metadata cannot be null!");
				}

				this.metadata = value;
			}
		}

		public ApplicationUrl Url 
		{
			get 
			{
				return this.url;
			}
			private set 
			{
				if (value == null) 
				{
					throw new ArgumentNullException ("value", "The application url cannot be null!");
				}

				this.url = value;
			}
		}

		public void UpdateMetadata(string name, string description) 
		{
			this.Metadata = new AppMetadata (name, description);
		}

		public void UpdateUrl(string url) 
		{
			this.Url = new ApplicationUrl (url);
		}
	}
}

