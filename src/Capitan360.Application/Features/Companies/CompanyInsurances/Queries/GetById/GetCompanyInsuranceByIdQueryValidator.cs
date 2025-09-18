using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capitan360.Application.Features.Companies.CompanyInsurances.Queries.GetByCompanyId;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyInsurances.Queries.GetById;
public class GetCompanyInsuranceByIdQueryValidator : AbstractValidator<GetCompanyInsuranceByIdQuery>
{
    public GetCompanyInsuranceByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه باید بزرگتر از صفر باشد");
    }
}