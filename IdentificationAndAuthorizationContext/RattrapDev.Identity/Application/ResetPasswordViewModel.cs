﻿using System;

namespace RattrapDev.Identity
{
	public class ResetPasswordViewModel
	{
		public Guid UserId { get; set; }
		public string CurrentPassword { get; set; }
		public string NewPassword { get; set; }
	}
}

