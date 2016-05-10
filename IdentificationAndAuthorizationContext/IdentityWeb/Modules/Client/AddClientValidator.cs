namespace IdentityWeb.Modules.Client
{
    using System;
    using System.Net.Mail;

    using FluentValidation;

    using Geonetric.Identity.Application;

    public class AddClientValidator : AbstractValidator<ClientViewModel>
	{
		public AddClientValidator() 
		{
			this.RuleFor (client => client.ClientName).NotEmpty ().WithMessage ("The client name is required!");
			this.RuleFor (client => client.ContactName).NotEmpty ().WithMessage ("The contact name is required!");
			this.RuleFor (client => client.ContactPhone).NotEmpty ().WithMessage ("The contact phone is required!");
			this.RuleFor (client => client.ContactEmail).NotEmpty ().WithMessage ("The contact email is required!");
			this.RuleFor (client => client.ContactEmail).Must ((email) => 
				{
					if (string.IsNullOrWhiteSpace(email)) 
						return true;
					try 
					{
						new MailAddress(email);
						return true;
					}
					catch (Exception) 
					{
						return false;
					}
				}).WithMessage ("The contact email must be a valid email!");
		}
	}
}

