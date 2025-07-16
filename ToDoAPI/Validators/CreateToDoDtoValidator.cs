using FluentValidation;
using ToDoApi.Models.DTOs;

namespace ToDoApi.Validators
{
    public class CreateToDoDtoValidator : AbstractValidator<CreateToDoDto>
    {
        public CreateToDoDtoValidator()
        {
            RuleFor(x => x.Task)
                .NotEmpty().WithMessage("Task description is required");

            RuleFor(x => x.IsCompleted)
                .NotNull().WithMessage("Completion status must be specified");
        }
    }
}