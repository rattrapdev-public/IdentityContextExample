namespace IdentityWeb.Modules.User
{
    using System;
    using System.Net.Mail;

    using FluentValidation;

    using Geonetric.Identity.Application;
    public class UserValidator : AbstractValidator<UserViewModel>
    {
        public UserValidator()
        {
            this.RuleFor(user => user.Username).NotEmpty().WithMessage("The username is required!");
            this.RuleFor(user => user.Password).NotEmpty().WithMessage("The password is required!");
            this.RuleFor(user => user.ClientId).NotEqual(Guid.Empty).WithMessage("The ClientId is required!");
            this.RuleFor(user => user.FirstName).NotEmpty().WithMessage("The first name is required!");
            this.RuleFor(user => user.LastName).NotEmpty().WithMessage("The last name is required!");
            this.RuleFor(user => user.Email).NotEmpty().WithMessage("The email is required!");
            this.RuleFor(user => user.Email).Must((email) =>
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
            }).WithMessage("The contact email must be a valid email!");
        }
    }
}