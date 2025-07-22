using System.ComponentModel.DataAnnotations;

namespace Capitan360.Domain.Constants;

public enum EntranceType
{
    [Display(Name = "بدون هزینه ورودی")]

    WithoutEntrance = 1,
     [Display(Name = "کم شدن وزن ورودی")]
    WithEntranceMinusEntranceWeight = 2,
     [Display(Name = "کم نشدن وزن ورودی")]

    WithEntrancePlusEntranceWeight = 3
}