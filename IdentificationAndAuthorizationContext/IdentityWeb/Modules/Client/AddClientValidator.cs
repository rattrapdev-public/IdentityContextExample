using System;
using FluentValidation;
using RattrapDev.Identity;

namespace IdentityWeb
{
	public class AddClientValidator : AbstractValidator<ClientViewModel>
	{
		public AddClientValidator() 
		{
			RuleFor (client => client.ClientName).NotEmpty ().WithMessage ("The client name is required!");
			RuleFor (client => client.ContactName).NotEmpty ().WithMessage ("The contact name is required!");
			RuleFor (client => client.ContactPhone).NotEmpty ().WithMessage ("The contact phone is required!");
		}
	}
}

