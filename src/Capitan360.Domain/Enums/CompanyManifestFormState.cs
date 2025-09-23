using System.ComponentModel.DataAnnotations;
using Capitan360.Domain.Constants;

namespace Capitan360.Domain.Enums;

public enum CompanyManifestFormState
{
    [Display(Name = ConstantNames.Ready)]
    Ready = 1,

    [Display(Name = ConstantNames.Issued)]
    Issued = 2,

    //[Display(Name = ConstantNames.Collectiong)]
    //Collectiong = 3,
    //
    //[Display(Name = ConstantNames.ReceivedAtSenderCompany)]
    //ReceivedAtSenderCompany = 4,
    //
    //[Display(Name = ConstantNames.Manifested)]
    //Manifested = 5,

    [Display(Name = ConstantNames.AirlineDelivery)]
    AirlineDelivery = 6,

    [Display(Name = ConstantNames.ReceivedAtReceiverCompany)]
    ReceivedAtReceiverCompany = 7,

    //[Display(Name = ConstantNames.Distribution)]
    //Distribution = 8,
    //
    //[Display(Name = ConstantNames.Delivery)]
    //Delivery = 9,
}
