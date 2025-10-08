using Capitan360.Application.Features.Identities.Users.Users.Dtos;
using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.Users.Queries.GetAll;

public class GetAllUserQueryValidator : AbstractValidator<GetAllUserQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];
    private string[] _allowedSortByColumnNames = [
        nameof(UserDto.NameFamily),
        nameof(UserDto.Mobile),
        nameof(UserDto.MobileTelegram),
        nameof(UserDto.Credit),
        nameof(UserDto.Active),
        nameof(UserDto.Baned)];

    public GetAllUserQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه شرکت باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.CompanyTypeId)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه نوع شرکت باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.RoleId)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه نقش باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.HasCredit)
            .InclusiveBetween(-1, 1).WithMessage("وضعیت اعتباری می تواند یکی از حالت مشتری اعتباری، مشتری غیر اعتباری و یا همه باشد.");

        RuleFor(x => x.Active)
            .InclusiveBetween(-1, 1).WithMessage("وضعیت فعالیت کاربر می تواند یکی از حالت فعال، غیر فعال و یا همه باشد.");

        RuleFor(x => x.Baned)
            .InclusiveBetween(-1, 1).WithMessage("وضعیت فعالیت کاربر می تواند یکی از حالت مسدود شده، مسدود نشده و یا همه باشد.");

        RuleFor(x => x.TypeOfFactorInSamanehMoadianId)
            .InclusiveBetween(-1, 2).WithMessage("وضعیت سامانه مودیان کاربر می تواند یکی از حقیقی، حقوقی، نا مشخص و یا همه باشد.");
        
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("شماره صفحه باید بزرگتر یا مساوی یک باشد");

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"تعداد ایتم در صفحه باید یکی از موارد زیر باشد [{string.Join(",", _allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"مرتب سازی باید بر اساس یکی از آیتم های زیر باشد [{string.Join(",", _allowedSortByColumnNames)}]");
    }
}