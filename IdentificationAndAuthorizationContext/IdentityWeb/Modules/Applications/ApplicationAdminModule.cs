using System;
using RattrapDev.Identity.Application;
using Nancy.ModelBinding;
using Nancy;
using Nancy.Validation;
using System.Collections.Generic;
using System.Linq;

namespace IdentityWeb
{
	public class ApplicationAdminModule : Nancy.NancyModule
	{
		public ApplicationAdminModule (IAppService appService)
			: base("/admin/apps")
		{
			Get ["/"] = parameters => 
			{
				var appResults = appService.GetAllApps();
				return View["Views/Admin/AppAdminSearch", appResults];
			};

			Post ["/"] = parameters => 
			{
				var viewModel = this.Bind<AppViewModel>();

				var validationResult = this.Validate(viewModel);
				var errorMessages = new List<string>();
				foreach (var errorResult in validationResult.Errors.Values) {
					errorMessages.AddRange(errorResult.Select(e => e.ErrorMessage));
				}

				if (!validationResult.IsValid) 
				{
					ViewBag.ErrorMessages = string.Join("|", errorMessages);
					return View["Views/Admin/AppAdminDetail", viewModel];
				}

				ViewBag.ErrorMessages = string.Join("|", errorMessages);

				viewModel = appService.SaveApp(viewModel);

				return Response.AsRedirect ("~/admin/apps/" + viewModel.Id + "?result=" + AppResult.SaveExistingApp);
			};

			Get ["/{AppIdentity}"] = parameters => 
			{
				Guid appIdentity;
				if (!(Guid.TryParse(parameters.AppIdentity, out appIdentity))) 
				{
					throw new ArgumentException("The App Identity must be a Guid");
				}

				string result = Request.Query.result.HasValue ? Request.Query.result : string.Empty;
				AppResult appResult;
				if (Enum.TryParse<AppResult>(result, out appResult)) 
				{
					ViewBag.ValidationMessage = AppMessageService.GetValidationMessage(appResult);
				}

				var appViewModel = appService.GetApp(appIdentity);

				return View["Views/Admin/AppAdminDetail", appViewModel];
			};

			Get ["/new"] = parameters => 
			{
				var emptyViewModel = new AppViewModel();
				return View["Views/Admin/AppAdminDetail", emptyViewModel];
			};
		}
	}
}

