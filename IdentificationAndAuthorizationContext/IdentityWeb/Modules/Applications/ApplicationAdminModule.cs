using System;
using RattrapDev.Identity.Application;

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

