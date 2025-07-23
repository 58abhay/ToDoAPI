using FluentValidation;
using ToDoAPI.Application.DTOs;

namespace ToDoAPI.Application.Validators
{
    public class UpdateTaskDtoValidator : AbstractValidator<UpdateTaskDto>
    {
        public UpdateTaskDtoValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Task description is required")
                .MaximumLength(200)
                .WithMessage("Description cannot exceed 200 characters");

            // `IsCompleted` is a non-nullable bool, so no need for .NotNull()
        }
    }
}