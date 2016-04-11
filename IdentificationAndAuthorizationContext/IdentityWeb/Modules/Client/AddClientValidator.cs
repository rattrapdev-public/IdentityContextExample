using System;
using FluentValidation;
using RattrapDev.Identity;
using System.Net.Mail;

namespace IdentityWeb
{
	public class AddClientValidator : AbstractValidator<ClientViewModel>
	{
		public AddClientValidator() 
		{
			RuleFor (client => client.ClientName).NotEmpty ().WithMessage ("The client name is required!");
			RuleFor (client => client.ContactName).NotEmpty ().WithMessage ("The contact name is required!");
			RuleFor (client => client.ContactPhone).NotEmpty ().WithMessage ("The contact phone is required!");
			RuleFor (client => client.ContactEmail).NotEmpty ().WithMessage ("The contact email is required!");
			RuleFor (client => client.ContactEmail).Must ((email) => 
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

