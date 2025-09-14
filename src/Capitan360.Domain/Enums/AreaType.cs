using System.ComponentModel.DataAnnotations;
using Capitan360.Domain.Constants;

namespace Capitan360.Domain.Enums;

public enum AreaType
{
    [Display(Name = ConstantNames.Country)]
    Country = 1,

    [Display(Name = ConstantNames.Province)]
    Province = 2,

    [Display(Name = ConstantNames.City)]
    City = 3,

    [Display(Name = ConstantNames.RegionMunicipality)]
    RegionMunicipality = 4
}
