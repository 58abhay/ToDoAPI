using FluentValidation;
using ToDoAPI.Application.DTOs;

namespace ToDoAPI.Application.Validators
{
    public class CreateToDoDtoValidator : AbstractValidator<CreateToDoDto>
    {
        public CreateToDoDtoValidator()
        {
            RuleFor(x => x.Task)
                .NotEmpty()
                .WithMessage("Task description is required")
                .MaximumLength(200);

            RuleFor(x => x.IsCompleted)
                .NotNull()
                .WithMessage("Completion status must be specified");
                
        }
    }
}