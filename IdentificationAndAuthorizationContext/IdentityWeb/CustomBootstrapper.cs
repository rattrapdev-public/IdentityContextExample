﻿using System;
using Nancy.Bootstrappers.Autofac;
using Autofac;
using Nancy.Bootstrapper;
using RattrapDev.Identity;
using Nancy;
using Nancy.Conventions;
using Nancy.Authentication.Forms;
using Nancy.Diagnostics;
using RattrapDev.Identity.Infrastructure.Applications;
using RattrapDev.Identity.Domain.Applications;
using RattrapDev.Identity.Application;

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
			builder.Register<ClientService> ().As<IClientService> ().SingleInstance;
			builder.Register<MockUserMapper> ().As<IUserMapper> ();
			builder.Register<AppInMemoryRepository> ().As<IAppRepository> ().SingleInstance;
			builder.Register<AppService> ().As<IAppService> ().SingleInstance;
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

		protected override DiagnosticsConfiguration DiagnosticsConfiguration {
			get {
				return new DiagnosticsConfiguration{ Password = "password" };
			}
		}
	}
}

