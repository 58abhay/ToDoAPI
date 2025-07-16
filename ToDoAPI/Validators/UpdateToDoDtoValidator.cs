using FluentValidation;
using ToDoApi.Models.DTOs;

namespace ToDoApi.Validators
{
    public class UpdateToDoDtoValidator : AbstractValidator<UpdateToDoDto>
    {
        public UpdateToDoDtoValidator()
        {
            RuleFor(x => x.Task)
                .NotEmpty().WithMessage("Task description is required");

            RuleFor(x => x.IsCompleted)
                .NotNull().WithMessage("Completion status must be specified");
        }
    }
}