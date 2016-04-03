using System;
using RattrapDev.DDD;

namespace RattrapDev.Identity.Domain.Application
{
	public class ApplicationMetadata : IValueObject
	{
		private string name;

		public ApplicationMetadata (string name, string description)
		{
			Name = name;
			Description = description;
		}

		public string Name 
		{
			get 
			{
				return name;
			}
			private set 
			{
				if (string.IsNullOrWhiteSpace (value)) 
				{
					throw new ArgumentException ("ApplicationMetadata.Name : A valid Application Name is required!");
				}

				name = value;
			}
		}

		public string Description { get; private set; }
	}
}

