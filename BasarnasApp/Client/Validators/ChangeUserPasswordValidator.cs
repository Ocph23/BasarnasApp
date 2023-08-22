using BasarnasApp.Shared.Models;
using FluentValidation;

namespace BasarnasApp.Client.Validators
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordRequest>
    {
        public ChangeUserPasswordValidator()
        {
            RuleFor((x) => x.Email).NotEmpty()
                .WithMessage("User Name/Email required !")
                .EmailAddress();

            RuleFor(x => x.NewPassword)
               .NotEmpty()
               .Length(8).WithMessage("Lenght of '{PropertyName}' Must 8.")
               .Matches("[A-Z]").WithMessage("'{PropertyName}' must contain one or more capital letters.")
               .Matches("[a-z]").WithMessage("'{PropertyName}' must contain one or more lowercase letters.")
               .Matches(@"\d").WithMessage("'{PropertyName}' must contain one or more digits.")
               .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("'{PropertyName}' must contain one or more special characters.")
               .Matches("^[^£# “”]*$").WithMessage("'{PropertyName}' must not contain the following characters £ # “” or spaces.");

            RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("Old Password Required");

            RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm Password Required").When(x=>!string.IsNullOrEmpty(x.NewPassword))
            .Equal(x=>x.NewPassword).WithMessage("Password Not Same !");

        }


        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ChangeUserPasswordRequest>.CreateWithOptions((ChangeUserPasswordRequest)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };

    }
}

