using Capitan360.Application.Features.ContentTypeService.Commands.UpdateActiveState;
using FluentValidation;

namespace Capitan360.Application.Features.ContentTypeService.Commands.UpdateActiveStateContentType
{
    public class
        UpdateActiveStateCompanyContentTypeValidator : AbstractValidator<UpdateActiveStateContentTypeCommand>
    {
        public UpdateActiveStateCompanyContentTypeValidator()
        {
            RuleFor(x => x.Id)
               .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگتر از صفر باشد");
        }
    }
}