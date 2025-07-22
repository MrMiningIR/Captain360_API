using FluentValidation;

namespace Capitan360.Application.Services.AddressService.Commands.MoveAddress;

public class MoveAddressDownValidator : AbstractValidator<MoveAddressDownCommand>
{
    public MoveAddressDownValidator()
    {
        RuleFor(x => x.AddressId)
            .GreaterThan(0).WithMessage("شناسه آدرس باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");
    }
}