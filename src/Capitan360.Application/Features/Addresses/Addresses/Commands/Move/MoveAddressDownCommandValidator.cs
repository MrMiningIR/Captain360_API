using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Addresses.Commands.Move;

public class MoveAddressDownCommandValidator : AbstractValidator<MoveAddressDownCommand>
{
    public MoveAddressDownCommandValidator()
    {
        RuleFor(x => x.AddressId)
            .GreaterThan(0).WithMessage("شناسه آدرس باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");
    }
}