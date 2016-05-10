using System;
using IdentityWeb;

namespace IdentityWebTests
{
    using Geonetric.Identity.Application;

    public class CustomTestBootstrapper : CustomBootstrapper
	{
		public CustomTestBootstrapper() : base() 
		{
		}

		public CustomTestBootstrapper(IClientService clientService) : base() 
		{
		}

		protected override void ApplicationStartup (Autofac.ILifetimeScope container, Nancy.Bootstrapper.IPipelines pipelines)
		{
			base.ApplicationStartup (container, pipelines);
		}

		public IClientService ClientService { get; private set; }
	}
}

