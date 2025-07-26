using System.ComponentModel.DataAnnotations;

namespace Capitan360.Domain.Constants;

public enum MoadianFactorType
{
    [Display(Name = "-")]
    None = 0,
    [Display(Name = "شخصی")]
    Person = 1,
    [Display(Name = "سازمانی")]

    Organization = 2,
    [Display(Name = "نامشخص")]
    Unknown = 3
}