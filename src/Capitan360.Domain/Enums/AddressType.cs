using System.ComponentModel.DataAnnotations;
using Capitan360.Domain.Constants;

namespace Capitan360.Domain.Enums;

public enum AddressType
{
    [Display(Name = ConstantNames.Ready)]
    UserAddress = 1,

    [Display(Name = ConstantNames.Ready)]
    CompanyAddress = 2
}