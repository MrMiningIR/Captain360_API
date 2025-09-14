using System.ComponentModel.DataAnnotations;
using Capitan360.Domain.Constants;

namespace Capitan360.Domain.Enums;

public enum WeightType
{
    [Display(Name = ConstantNames.WeightTypeMinName)]
    TypeMin = -1,

    [Display(Name = ConstantNames.WeightTypeNormalName)]
    TypeNormal = 0,

    [Display(Name = ConstantNames.WeightType1Name)]
    TypeOne = 1,

    [Display(Name = ConstantNames.WeightType2Name)]
    TypeTwo = 2,

    [Display(Name = ConstantNames.WeightType3Name)]
    TypeThree = 3,

    [Display(Name = ConstantNames.WeightType4Name)]
    TypeFour = 4,

    [Display(Name = ConstantNames.WeightType5Name)]
    TypeFive = 5,

    [Display(Name = ConstantNames.WeightType6Name)]
    TypeSix = 6,

    [Display(Name = ConstantNames.WeightType7Name)]
    TypeSeven = 7,

    [Display(Name = ConstantNames.WeightType8Name)]
    TypeEight = 8,

    [Display(Name = ConstantNames.WeightType9Name)]
    TypeNine = 9,

    [Display(Name = ConstantNames.WeightType10Name)]
    TypeTen = 10,

    [Display(Name = ConstantNames.WeightType11Name)]
    TypeEleven = 11
}
