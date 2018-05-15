using Microsoft.VisualStudio.TestTools.UnitTesting;
using Finanzas.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas.Helpers.Tests
{
    [TestClass()]
    public class FinanzasTests
    {
        [TestMethod()]
        public void HallarTEPTest()
        {
            double result = Finanzas.HallarTEP(0.24, 360, 90, null);
            Assert.AreEqual(0.0552501, result);
        }

        [TestMethod()]
        public void HallarTEPTest2()
        {
            double result = Finanzas.HallarTEP(0.08, 360, 30, 180);
            Assert.AreEqual(0.0065582, result);
        }

        [TestMethod()]
        public void HallarTEATest()
        {
            double result = Finanzas.HallarTEA(0.08, 360, 30);
            Assert.AreEqual(0.0829995, result);
        }

        [TestMethod()]
        public void HallarCOKTest()
        {
            double result = Finanzas.HallarCOK(0.2, 360, 30);
            Assert.AreEqual(0.0153095, result);
        }

        [TestMethod()]
        public void HallarCostesInicialesTest()
        {
            double result = Finanzas.HallarCostesIniciales(1050, 0.01, 0.0025, 0.0045, 0.005);
            Assert.AreEqual(23.10, result);
        }
    }
}