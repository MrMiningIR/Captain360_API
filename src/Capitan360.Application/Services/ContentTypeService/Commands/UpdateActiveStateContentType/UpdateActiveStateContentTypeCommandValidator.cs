using FluentValidation;

namespace Capitan360.Application.Services.ContentTypeService.Commands.UpdateActiveStateContentType
{
    public class
        UpdateActiveStateCompanyContentTypeValidator : AbstractValidator<UpdateActiveStateContentTypeCommand>
    {
        public UpdateActiveStateCompanyContentTypeValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("شناسه محتوی باید بزرگ‌تر از صفر باشد");
        }
    }
}