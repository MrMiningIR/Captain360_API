using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capitan360.Domain.Constants;

public enum ManifestFormState
{
    Ready = 1,
    Issued = 2,
    //Collectiong = 3,
    //ReceivedAtSenderCompany = 4,
    //Manifested = 5,
    AirlineDelivery = 6,
    ReceivedAtReceiverCompany = 7,
    //Distribution = 8,
    //Delivery = 9,
}
