using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Addresses.Commands.MoveDown;

public class MoveDownAddressCommandValidator : AbstractValidator<MoveDownAddressCommand>
{
    public MoveDownAddressCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه آدرس باید بزرگ‌تر از صفر باشد");
    }
}