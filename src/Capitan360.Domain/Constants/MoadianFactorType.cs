using System.ComponentModel.DataAnnotations;

namespace Capitan360.Domain.Constants;

public enum MoadianFactorType
{
    [Display(Name = "حقیقی")]
    Haghigh = 1,

    [Display(Name = "حقوقی")]
    Hoghoghi = 2,

    [Display(Name = "نامشخص")]
    Unknown = 3
}