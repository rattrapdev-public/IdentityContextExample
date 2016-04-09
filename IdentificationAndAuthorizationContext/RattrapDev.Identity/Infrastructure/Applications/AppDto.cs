using System;

namespace RattrapDev.Identity.Infrastructure.Applications
{
	public class AppDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string BaseUrl { get; set; }
	}
}

