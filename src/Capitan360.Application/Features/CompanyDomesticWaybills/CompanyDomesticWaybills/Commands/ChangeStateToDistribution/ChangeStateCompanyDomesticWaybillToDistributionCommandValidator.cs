using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDistribution;

public class ChangeStateCompanyDomesticWaybillToDistributionCommandValidator : AbstractValidator<ChangeStateCompanyDomesticWaybillToDistributionCommand>
{
    public ChangeStateCompanyDomesticWaybillToDistributionCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه بزرگتر از صفر باشد");

        RuleFor(x => x.BikeDeliveryInReceiverCompanyId)
            .NotEmpty().WithMessage("شماره شناسایی پیک الزامی است")
            .MaximumLength(450).WithMessage("شماره شناسایی پیک نمی‌تواند بیشتر از 450 کاراکتر باشد");

        RuleFor(x => x.BikeDeliveryInReceiverCompanyAgent)
            .NotEmpty().WithMessage("نام و نام پیک الزامی است")
            .MaximumLength(100).WithMessage("نام و نام خانوادگی پیک نمی‌تواند بیشتر از 100 کاراکتر باشد");
    }
}
