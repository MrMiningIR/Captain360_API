using FluentValidation;

namespace Capitan360.Application.Services.AddressService.Commands.MoveAddress;

public class MoveAddressUpValidator : AbstractValidator<MoveAddressUpCommand>
{
    public MoveAddressUpValidator()
    {
        RuleFor(x => x.AddressId)
            .GreaterThan(0).WithMessage("شناسه آدرس باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");
    }
}