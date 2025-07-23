using FluentValidation;
using ToDoAPI.Application.DTOs;

namespace ToDoAPI.Application.Validators
{
    public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskDtoValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Task description is required")
                .MaximumLength(200)
                .WithMessage("Description cannot exceed 200 characters");

            // Optional: FluentValidation doesn't enforce bool nullability,
            // but we can check if boolean is unset if you use nullable bools in future.
        }
    }
}