using FluentValidation;
using ToDoAPI.Application.DTOs;

namespace ToDoAPI.Application.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.UserEmail)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email must be valid");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters");
        }
    }
}