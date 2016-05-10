namespace IdentityWeb.Modules.User
{
    using System;
    using System.Dynamic;
    using System.Linq;

    using Geonetric.Identity.Application;

    using Nancy;
    using Nancy.ModelBinding;

    public class UserAdminModule : NancyModule
	{
		public UserAdminModule (IUserService userService, IClientService clientService) : base("/admin/users")
		{
			this.Get ["/"] = parameters => 
			{
				var userResults = userService.GetAllUsers();
				return this.View["Views/Admin/UserAdminSearch", userResults];
			};

			this.Post ["/"] = parameters => 
			{
				var viewModel = this.Bind<UserViewModel>();

				viewModel = userService.SaveUser(viewModel);

				return this.Response.AsRedirect("~/admin/users/" + viewModel.UserId + "?result=" + UserResult.SaveUser);
			};

			this.Post ["/resetpassword"] = parameters =>
			{
				var viewModel = this.Bind<ResetPasswordViewModel>();

				userService.ResetPassword(viewModel);

				return this.Response.AsRedirect("~/admin/users/" + viewModel.UserId + "?result=" + UserResult.ResetUserPassword);
			};

			this.Get ["/{UserId:Guid}"] = parameters => 
			{
				dynamic viewModel = new ExpandoObject();

				Guid userId = (Guid)parameters.UserId;
				var userViewModel = userService.GetUser(userId);
				viewModel.User = userViewModel;

				var clients = clientService.GetAll();
				viewModel.Client = clients.First(c => c.ClientIdentity.Equals(userViewModel.ClientId));

				string result = this.Request.Query.result.HasValue ? this.Request.Query.result : string.Empty;
				UserResult userResult;
				if (Enum.TryParse<UserResult>(result, out userResult)) 
				{
					this.ViewBag.ValidationMessage = UserMessageService.GetValidationMessage(userResult);
				}

				return this.View["Views/Admin/UserAdminDetail", viewModel];
			};

			this.Get ["/new"] = parameters => 
			{
				dynamic viewModel = new ExpandoObject();
				var emptyViewModel = new UserViewModel();

				var clientViewModels = clientService.GetAll();

				viewModel.Clients = clientViewModels;
				viewModel.User = emptyViewModel;

				return this.View["Views/Admin/UserAdminDetail", viewModel];
			};
		}
	}
}

