using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesChallengeApi.Application.Trolleys.Model
{
    public class Special
    {
        public List<QuantityValue> Quantities { get; set; } = new List<QuantityValue>();

        public double Total { get; set; }
    }
}
