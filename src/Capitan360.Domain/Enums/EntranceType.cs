using System.ComponentModel.DataAnnotations;
using Capitan360.Domain.Constants;

namespace Capitan360.Domain.Enums;

public enum EntranceFeeType
{
    [Display(Name = ConstantNames.WithoutEntrance)]
    WithoutEntrance = 1,

    [Display(Name = ConstantNames.WithEntranceMinusEntranceFeeWeight)]
    WithEntranceMinusEntranceFeeWeight = 2,

    [Display(Name = ConstantNames.WithEntrancePlusEntranceFeeWeight)]
    WithEntrancePlusEntranceFeeWeight = 3
}