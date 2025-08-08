using FluentValidation;

namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateActiveStateCompanyContentType
{
    public class UpdateActiveStateCompanyContentTypeValidator : AbstractValidator<UpdateActiveStateCompanyContentTypeCommand>
    {
        public UpdateActiveStateCompanyContentTypeValidator()
        {
            RuleFor(x => x.Id)
                 .GreaterThan(0).WithMessage("شناسه نوع محتوی شرکت الزامی است");
        }
    }
}