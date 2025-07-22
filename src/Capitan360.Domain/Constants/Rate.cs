using System.ComponentModel.DataAnnotations;

namespace Capitan360.Domain.Constants;

public enum Rate
{
    [Display(Name = ConstantNames.RateOne)]
    R1 = 1,
    [Display(Name = ConstantNames.RateTwo)]
    R2 = 2,
    [Display(Name = ConstantNames.RateThree)]
    R3 = 3,
    [Display(Name = ConstantNames.RateFour)]
    R4 = 4,
    [Display(Name = ConstantNames.RateFive)]
    R5 = 5,
    [Display(Name = ConstantNames.RateSix)]
    R6 = 6,
    [Display(Name = ConstantNames.RateSeven)]
    R7 = 7,
    [Display(Name = ConstantNames.RateEight)]
    R8 = 8,
    [Display(Name = ConstantNames.RateNine)]
    R9 = 9,
    [Display(Name = ConstantNames.RateTen)]
    R10 = 10
}