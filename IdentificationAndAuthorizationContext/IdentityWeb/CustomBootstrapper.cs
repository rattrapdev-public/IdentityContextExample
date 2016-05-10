using Nancy.Bootstrappers.Autofac;
using Autofac;
using Nancy.Bootstrapper;
using Nancy;
using Nancy.Conventions;
using Nancy.Authentication.Forms;
using Nancy.Diagnostics;

namespace IdentityWeb
{
    using Geonetric.Identity.Application;
    using Geonetric.Identity.Domain.Applications;
    using Geonetric.Identity.Domain.Clients;
    using Geonetric.Identity.Domain.Users;
    using Geonetric.Identity.Infrastructure.Applications;
    using Geonetric.Identity.Infrastructure.Client;
    using Geonetric.Identity.Infrastructure.Users;

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

			StaticConfiguration.DisableErrorTraces = false;

			var formsAuthConfig = new FormsAuthenticationConfiguration () 
			{
				RedirectUrl = "~/login",
				UserMapper = container.Resolve<IUserMapper>(),
			};

			FormsAuthentication.Enable (pipelines, formsAuthConfig);
		}

		protected override void ConfigureApplicationContainer (ILifetimeScope existingContainer)
		{
			var builder = new ContainerBuilder ();
			builder.RegisterInstance (clientService).As<IClientService> ().SingleInstance();
			builder.RegisterType<ClientInMemoryRepository> ().As<IClientRepository>().SingleInstance();
			builder.RegisterType<MockUserMapper> ().As<IUserMapper> ();
			builder.RegisterType<AppInMemoryRepository> ().As<IAppRepository> ().SingleInstance();
			builder.RegisterType<AppService> ().As<IAppService> ().SingleInstance();
			builder.RegisterType<UserInMemoryRepository> ().As<IUserRepository> ().SingleInstance();
			builder.RegisterType<UserService> ().As <IUserService> ().SingleInstance();

			builder.Update (existingContainer.ComponentRegistry);
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

		protected override DiagnosticsConfiguration DiagnosticsConfiguration {
			get {
				return new DiagnosticsConfiguration{ Password = "password" };
			}
		}
	}
}

