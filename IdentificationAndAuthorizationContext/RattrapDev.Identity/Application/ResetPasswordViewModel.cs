namespace Geonetric.Identity.Application
{
    using System;

    public class ResetPasswordViewModel
	{
		public Guid UserId { get; set; }
		public string NewPassword { get; set; }
		public string ConfirmPassword { get; set; }
		public string CurrentPassword { get; set; }
	}
}

