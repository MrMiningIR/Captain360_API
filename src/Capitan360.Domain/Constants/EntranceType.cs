using System.ComponentModel.DataAnnotations;

namespace Capitan360.Domain.Constants;

public enum EntranceFeeType
{
    [Display(Name = "بدون هزینه ورودی")]

    WithoutEntrance = 1,
    [Display(Name = "کم شدن وزن ورودی")]
    WithEntranceMinusEntranceFeeWeight = 2,
    [Display(Name = "کم نشدن وزن ورودی")]

    WithEntrancePlusEntranceFeeWeight = 3
}