using System;
using Nancy.Bootstrappers.Autofac;
using Autofac;
using Nancy.Bootstrapper;
using RattrapDev.Identity;
using Nancy;
using Nancy.Conventions;
using Nancy.Authentication.Forms;

namespace IdentityWeb
{
	public class CustomBootstrapper : AutofacNancyBootstrapper
	{
		private IClientService clientService;

		public CustomBootstrapper() 
		{
			clientService = new ClientService ();
		}

		public CustomBootstrapper(IClientService clientService) 
		{
			this.clientService = clientService;
		}

		protected override void ApplicationStartup (ILifetimeScope container, IPipelines pipelines)
		{
			base.ApplicationStartup (container, pipelines);
			var builder = new ContainerBuilder ();
			builder.RegisterInstance<IClientService> (clientService);
			builder.RegisterInstance<IUserMapper> (new MockUserMapper ());
			builder.Update (container.ComponentRegistry);

			StaticConfiguration.DisableErrorTraces = false;

			var formsAuthConfig = new FormsAuthenticationConfiguration () 
			{
				RedirectUrl = "~/login",
				UserMapper = container.Resolve<IUserMapper>(),
			};

			FormsAuthentication.Enable (pipelines, formsAuthConfig);
		}

		protected override void ConfigureConventions (Nancy.Conventions.NancyConventions nancyConventions)
		{
			base.ConfigureConventions (nancyConventions);
			nancyConventions.StaticContentsConventions.Add (
				StaticContentConventionBuilder.AddDirectory("Scripts")
			);
			nancyConventions.StaticContentsConventions.Add (
				StaticContentConventionBuilder.AddDirectory("Data")
			);
		}
	}
}

