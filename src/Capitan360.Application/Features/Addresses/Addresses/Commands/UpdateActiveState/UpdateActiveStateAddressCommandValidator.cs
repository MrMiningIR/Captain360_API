using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Addresses.Commands.UpdateActiveState;

public class UpdateActiveStateAddressCommandValidator : AbstractValidator<UpdateActiveStateAddressCommand>
{
    public UpdateActiveStateAddressCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه آدرس باید بزرگ‌تر از صفر باشد");
    }
}
