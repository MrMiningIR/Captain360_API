using Capitan360.Application.Extensions;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.DeleteEntrance;

public class DeleteEntranceDomesticWaybillFromCaptainCargoDesktopCommandValidator : AbstractValidator<DeleteEntranceDomesticWaybillFromCaptainCargoDesktopCommand>
{
    public DeleteEntranceDomesticWaybillFromCaptainCargoDesktopCommandValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره بارنامه باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanyReceiverCaptain360Code)
            .NotNull().WithMessage("کد کاپیتان 360 شرکت گیرنده  نمی‌تواند خالی باشد")
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت گیرنده الزامی است")
            .MaximumLength(10).WithMessage("کد کاپیتان 360 شرکت گیرنده نمی‌تواند بیشتر از 10 کاراکتر باشد");
    }
}
