using System.ComponentModel.DataAnnotations;
using Capitan360.Domain.Constants;

namespace Capitan360.Domain.Enums;

public enum MoadianFactorType
{
    [Display(Name = ConstantNames.Haghigh)]
    Haghigh = 1,

    [Display(Name = ConstantNames.Hoghoghi)]
    Hoghoghi = 2,

    [Display(Name = ConstantNames.Unknown)]
    Unknown = 3
}