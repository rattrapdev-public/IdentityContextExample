using System;
using RattrapDev.DDD;
using RattrapDev.Identity.Infrastructure.Applications;

namespace RattrapDev.Identity.Domain.Applications
{
	public class App : IAggregate
	{
		private AppIdentifier identifier;
		private AppMetadata metadata;
		private ApplicationUrl url;

		public App(string name, string description, string url) 
		{
			Identifier = new AppIdentifier ();
			Metadata = new AppMetadata (name, description);
			Url = new ApplicationUrl (url);
		}

		public App(ApplicationDto dto) 
		{
			Identifier = new AppIdentifier (dto.Id);
			Metadata = new AppMetadata (dto.Name, dto.Description);
			Url = new ApplicationUrl (dto.BaseUrl);
		}

		public AppIdentifier Identifier 
		{
			get 
			{
				return identifier;
			}
			private set 
			{
				if (value == null) 
				{
					throw new ArgumentNullException ("value", "The identifier cannot be null!");
				}

				identifier = value;
			}
		}

		public AppMetadata Metadata 
		{
			get 
			{
				return metadata;
			}
			private set 
			{
				if (value == null) 
				{
					throw new ArgumentNullException ("value", "The application metadata cannot be null!");
				}

				metadata = value;
			}
		}

		public ApplicationUrl Url 
		{
			get 
			{
				return url;
			}
			private set 
			{
				if (value == null) 
				{
					throw new ArgumentNullException ("value", "The application url cannot be null!");
				}

				url = value;
			}
		}

		public void UpdateMetadata(string name, string description) 
		{
			Metadata = new AppMetadata (name, description);
		}

		public void UpdateUrl(string url) 
		{
			Url = new ApplicationUrl (url);
		}
	}
}

