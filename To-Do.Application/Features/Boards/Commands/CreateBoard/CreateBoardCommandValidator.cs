using FluentValidation;

namespace To_Do.Application.Features.Boards.Commands.CreateBoard;

public sealed class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
{
    public CreateBoardCommandValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty()
            .WithMessage("Board name is required")
            .MaximumLength(50)
            .WithMessage("Board name must not exceed 50 characters");
    }
}