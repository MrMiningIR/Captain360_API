using System.ComponentModel.DataAnnotations;

namespace Capitan360.Domain.Enums;

public enum UserKind
{
    [Display(Name = "همه")]
    All = 0,

    [Display(Name = "کاربر ویژه")]
    Special = 1,

    [Display(Name = "کاربر عادی")]
    Normal = 2
}