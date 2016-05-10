namespace IdentityWeb.Modules.Applications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Geonetric.Identity.Application;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Validation;

    public class ApplicationAdminModule : Nancy.NancyModule
	{
		public ApplicationAdminModule (IAppService appService)
			: base("/admin/apps")
		{
			this.Get ["/"] = parameters => 
			{
				var appResults = appService.GetAllApps();
				return this.View["Views/Admin/AppAdminSearch", appResults];
			};

			this.Post ["/"] = parameters => 
			{
				var viewModel = this.Bind<AppViewModel>();

				var validationResult = this.Validate(viewModel);
				var errorMessages = new List<string>();
				foreach (var errorResult in validationResult.Errors.Values) {
					errorMessages.AddRange(errorResult.Select(e => e.ErrorMessage));
				}

				if (!validationResult.IsValid) 
				{
					this.ViewBag.ErrorMessages = string.Join("|", errorMessages);
					return this.View["Views/Admin/AppAdminDetail", viewModel];
				}

				this.ViewBag.ErrorMessages = string.Join("|", errorMessages);

				viewModel = appService.SaveApp(viewModel);

				return this.Response.AsRedirect ("~/admin/apps/" + viewModel.Id + "?result=" + AppResult.SaveExistingApp);
			};

			this.Get ["/{AppIdentity}"] = parameters => 
			{
				Guid appIdentity;
				if (!(Guid.TryParse(parameters.AppIdentity, out appIdentity))) 
				{
					throw new ArgumentException("The App Identity must be a Guid");
				}

				string result = this.Request.Query.result.HasValue ? this.Request.Query.result : string.Empty;
				AppResult appResult;
				if (Enum.TryParse<AppResult>(result, out appResult)) 
				{
					this.ViewBag.ValidationMessage = AppMessageService.GetValidationMessage(appResult);
				}

				var appViewModel = appService.GetApp(appIdentity);

				return this.View["Views/Admin/AppAdminDetail", appViewModel];
			};

			this.Get ["/new"] = parameters => 
			{
				var emptyViewModel = new AppViewModel();
				return this.View["Views/Admin/AppAdminDetail", emptyViewModel];
			};
		}
	}
}

