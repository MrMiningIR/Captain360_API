using FluentValidation;
using System.Linq.Expressions;

namespace Capitan360.Application.Services.ContentTypeService;

public abstract class BaseContentTypeValidator<T> : AbstractValidator<T>
{
    protected void ApplyCommonRules(Expression<Func<T, int>> companyTypeIdExpr,
                                    Expression<Func<T, string>> nameExpr,
                                    Expression<Func<T, string>> descriptionExpr)
    {
        RuleFor(companyTypeIdExpr)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت باید بزرگ‌تر از صفر باشد");

        RuleFor(nameExpr)
            .NotEmpty().WithMessage("نام بسته بندی الزامی است")
            .MaximumLength(50).WithMessage("نام محتوی نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(descriptionExpr)
            .MaximumLength(500).WithMessage("توضیحات محتوی نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}