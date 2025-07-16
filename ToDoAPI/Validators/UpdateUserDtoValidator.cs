using FluentValidation;
using ToDoApi.Models.DTOs;

namespace ToDoApi.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.UserEmail)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be valid");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}