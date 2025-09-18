using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Addresses.Commands.Delete;

public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
{
    public DeleteAddressCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه آدرس باید بزرگ‌تر از صفر باشد");
    }
}