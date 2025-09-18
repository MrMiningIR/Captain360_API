using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Areas.Commands.Delete;

public class DeleteAreaCommandValidator : AbstractValidator<DeleteAreaCommand>
{
    public DeleteAreaCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه منطقه باید بزرگ‌تر از صفر باشد");
    }
}