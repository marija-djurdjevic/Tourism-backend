using Explorer.BuildingBlocks.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Tests
{
    //Ovu klasu nasljedjuju svi integracioni testovi
    public class BasePaymentsIntegrationTest : BaseWebIntegrationTest<PaymentsTestFactory>
    {
        public BasePaymentsIntegrationTest(PaymentsTestFactory factory) : base(factory) { }
    }
}
