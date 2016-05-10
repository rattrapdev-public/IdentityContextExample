namespace Geonetric.Identity.Infrastructure.Applications
{
    using System;

    public class AppDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string BaseUrl { get; set; }
	}
}

