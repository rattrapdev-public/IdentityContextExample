using System;
using Nancy;
using RattrapDev.Identity.Application;
using Nancy.ModelBinding;

namespace IdentityWeb
{
	public class UserAdminModule : NancyModule
	{
		public UserAdminModule (IUserService userService) : base("/admin/users")
		{
			Get ["/"] = parameters => 
			{
				var userResults = userService.GetAllUsers();
				return View["Views/Admin/UserAdminSearch", userResults];
			};

			Post ["/"] = parameters => 
			{
				var viewModel = this.Bind<UserViewModel>();

				viewModel = userService.SaveUser(viewModel);

				return Response.AsRedirect("~/admin/user/" + viewModel.UserId);
			};

			Get ["/{UserId:Guid}"] = parameters => 
			{
				Guid userId = (Guid)parameters.UserId;
				var userViewModel = userService.GetUser(userId);

				return View["Views/Admin/UserAdminDetail", userViewModel];
			};

			Get ["/new"] = parameters => 
			{
				var emptyViewModel = new UserViewModel();
				return View["Views/Admin/UserAdminDetail", emptyViewModel];
			};
		}
	}
}

