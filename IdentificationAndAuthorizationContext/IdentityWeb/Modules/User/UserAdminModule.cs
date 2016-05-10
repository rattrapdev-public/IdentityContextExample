using System;
using Nancy;
using RattrapDev.Identity.Application;
using Nancy.ModelBinding;
using RattrapDev.Identity;
using System.Linq;
using System.Dynamic;

namespace IdentityWeb
{
	public class UserAdminModule : NancyModule
	{
		public UserAdminModule (IUserService userService, IClientService clientService) : base("/admin/users")
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

				return Response.AsRedirect("~/admin/users/" + viewModel.UserId);
			};

			Post ["/resetpassword"] = parameters =>
			{
				var viewModel = this.Bind<ResetPasswordViewModel>();

				userService.ResetPassword(viewModel);

				return Response.AsRedirect("~/admin/users/" + viewModel.UserId);
			};

			Get ["/{UserId:Guid}"] = parameters => 
			{
				dynamic viewModel = new ExpandoObject();

				Guid userId = (Guid)parameters.UserId;
				var userViewModel = userService.GetUser(userId);
				viewModel.User = userViewModel;

				var clients = clientService.GetAll();
				viewModel.Client = clients.First(c => c.ClientIdentity.Equals(userViewModel.ClientId));

				return View["Views/Admin/UserAdminDetail", viewModel];
			};

			Get ["/new"] = parameters => 
			{
				dynamic viewModel = new ExpandoObject();
				var emptyViewModel = new UserViewModel();

				var clientViewModels = clientService.GetAll();

				viewModel.Clients = clientViewModels;
				viewModel.User = emptyViewModel;

				return View["Views/Admin/UserAdminDetail", viewModel];
			};
		}
	}
}

