using System;
using IdentityWeb;
using RattrapDev.Identity;

namespace IdentityWebTests
{
	public class CustomTestBootstrapper : CustomBootstrapper
	{
		private CustomRootPathProvider customRootPathProvider = new CustomRootPathProvider();

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

		protected override Nancy.IRootPathProvider RootPathProvider {
			get {
				return customRootPathProvider;
			}
		}

		public IClientService ClientService { get; private set; }
	}
}

