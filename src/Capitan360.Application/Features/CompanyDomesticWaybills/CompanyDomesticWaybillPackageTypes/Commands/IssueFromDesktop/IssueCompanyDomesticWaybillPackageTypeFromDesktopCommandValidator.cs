using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Commands.IssueFromDesktop;

public class IssueCompanyDomesticWaybillPackageTypeFromDesktopCommandValidator : AbstractValidator<IssueCompanyDomesticWaybillPackageTypeFromDesktopCommand>
{
    public IssueCompanyDomesticWaybillPackageTypeFromDesktopCommandValidator()
    {
        RuleFor(x => x.CompanyDomesticWaybillId)
           .GreaterThan(0).WithMessage("شناسه بارنامه باید بزرگتر از صفر باشد");

        RuleFor(x => x.UserInsertedPackageName)
           .NotNull().WithMessage("اطلاعات بیسته بندی نمی تواند خالی باشد است.")
           .MaximumLength(30).WithMessage("اطلاعات بیسته بندی نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.UserInsertedContentName)
           .NotNull().WithMessage("اطلاعات محتوی بار نمی تواند خالی باشد است.")
           .MaximumLength(30).WithMessage("اطلاعات محتوی بار نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.GrossWeight)
           .GreaterThan(0).WithMessage("وزن محاسبه شده باید بزرگتر از صفر باشد")
           .PrecisionScale(10, 2, true).WithMessage("وزن محاسبه شده باید حداکثر 2 رقم اعشار و مجموعاً 10 رقم داشته باشد.");

        RuleFor(x => x.DimensionalWeight)
           .GreaterThanOrEqualTo(0).WithMessage("وزن ابعادی باید بزرگتر یا مساوری صفر باشد")
           .PrecisionScale(10, 2, true).WithMessage("وزن ابعادی باید حداکثر 2 رقم اعشار و مجموعاً 10 رقم داشته باشد.");

        RuleFor(x => x.DeclaredValue)
            .NotEmpty().WithMessage("ارزش کالا الزامی است")
            .MaximumLength(100).WithMessage("ارزش کالا نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.Dimensions)
          .NotEmpty().WithMessage("ابعاد کالا الزامی است.")
          .MaximumLength(14).WithMessage("ابعاد کالا نمی‌تواند بیشتر از 14 کاراکتر باشد.")
          .Matches(@"^\d{1,3}\*\d{1,3}\*\d{1,3}$")
              .WithMessage("ابعاد باید در قالب «طول*عرض*ارتفاع» با هر بخش 1 تا 3 رقم باشد (مثال: 20*30*15).");

        RuleFor(x => x.CountDimension)
            .GreaterThan(0).WithMessage("تعداد مرسولات باید بزرگتر از صفر باشد");
    }
}

