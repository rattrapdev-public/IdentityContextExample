namespace IdentityWeb.Modules.Applications
{
    using System;

    using FluentValidation;

    using Geonetric.Identity.Application;

    public class AddAppValidator : AbstractValidator<AppViewModel>
	{
		public AddAppValidator ()
		{
			this.RuleFor (app => app.Name).NotEmpty ().WithMessage ("The app name is required!");
			this.RuleFor (app => app.Url).NotEmpty ().WithMessage ("The app url is required!");
			this.RuleFor (app => app.Url).Must ((url) => 
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

