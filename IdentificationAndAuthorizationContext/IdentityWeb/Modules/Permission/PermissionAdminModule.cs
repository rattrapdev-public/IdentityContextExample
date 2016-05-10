namespace IdentityWeb.Modules.Permission
{
    using Nancy;

    public class PermissionAdminModule : NancyModule
	{
		public PermissionAdminModule () : base("/admin/permissions")
		{
			this.Get ["/"] = parameters => 
			{
				return this.View["Views/Admin/PermissionAdminSearch"];
			};
		}
	}
}

