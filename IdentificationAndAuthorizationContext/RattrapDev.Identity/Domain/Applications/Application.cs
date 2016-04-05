using System;
using RattrapDev.DDD;
using RattrapDev.Identity.Infrastructure.Applications;

namespace RattrapDev.Identity.Domain.Applications
{
	public class Application : IAggregate
	{
		private ApplicationIdentifier identifier;
		private ApplicationMetadata metadata;
		private ApplicationUrl url;

		public Application(string name, string description, string url) 
		{
			Identifier = new ApplicationIdentifier ();
			Metadata = new ApplicationMetadata (name, description);
			Url = new ApplicationUrl (url);
		}

		public Application(ApplicationDto dto) 
		{
			Identifier = new ApplicationIdentifier (dto.Id);
			Metadata = new ApplicationMetadata (dto.Name, dto.Description);
			Url = new ApplicationUrl (dto.BaseUrl);
		}

		public ApplicationIdentifier Identifier 
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

		public ApplicationMetadata Metadata 
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
			Metadata = new ApplicationMetadata (name, description);
		}

		public void UpdateUrl(string url) 
		{
			Url = new ApplicationUrl (url);
		}
	}
}

