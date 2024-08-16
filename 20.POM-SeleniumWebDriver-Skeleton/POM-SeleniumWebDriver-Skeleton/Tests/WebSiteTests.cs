using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_SeleniumWebDriver_Skeleton.Tests
{
    public class WebSiteTests : BaseTest
    {
        [Test, Order(1)]
        public void CreateRevueWithInvalidDataTest()
        {
            Assert.Pass();
        }

        [Test, Order(2)]
        public void CreateRevueWithValidDataTest()
        {
            Assert.Pass();
        }
    }
}
