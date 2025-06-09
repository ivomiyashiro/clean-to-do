using FluentValidation;

namespace To_Do.Application.Features.Boards.Commands.CreateBoard;

public sealed class CreateBoardValidator : AbstractValidator<CreateBoardDTO>
{
    public CreateBoardValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Board name is required")
            .MaximumLength(50)
            .WithMessage("Board name must not exceed 50 characters");
    }
}