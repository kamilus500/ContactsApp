using FluentValidation;
using FluentValidation.Validators;

namespace ContactsApp.Application.Contact.Commands.CreateContact
{
    public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
    {
        public CreateContactCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name should not be empty")
                .MinimumLength(3).WithMessage("First name should have at least 3 characters")
                .MaximumLength(15).WithMessage("First name should have maximum of 15 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name should not be empty")
                .MinimumLength(3).WithMessage("Last name should have at least 3 characters")
                .MaximumLength(20).WithMessage("Last name should have maxium of 20 characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible);

            RuleFor(x => x.NumberPhone)
                .NotEmpty().WithMessage("Number phone should not be empty")
                .MinimumLength(9).WithMessage("Number phone should have 9 numbers")
                .MaximumLength(9).WithMessage("Number phone should have 9 numbers");

        }
    }
}
