using System;
using FluentValidation;
using RattrapDev.Identity.Application;

namespace IdentityWeb
{
	public class AddAppValidator : AbstractValidator<AppViewModel>
	{
		public AddAppValidator ()
		{
			RuleFor (app => app.Name).NotEmpty ().WithMessage ("The app name is required!");
			RuleFor (app => app.Url).NotEmpty ().WithMessage ("The app url is required!");
			RuleFor (app => app.Url).Must ((url) => 
				{
					if (url == null) 
						return true;
					try 
					{
						new Uri(url);
						return true;
					}
					catch (UriFormatException) 
					{
						return false;
					}
				}).WithMessage ("The url must be a valid url!");
		}
	}
}

