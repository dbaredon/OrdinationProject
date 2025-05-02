using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using shared.Model;

namespace ordination_test
{
    [TestClass]
    public class PNTests
    {
        [TestMethod]
        public void givDosis_ValidStartDate_ReturnsTrue()
        {
            var pn = new PN(DateTime.Today, DateTime.Today.AddDays(5), 1.0, new Laegemiddel());
            var dato = new Dato { dato = DateTime.Today };

            var result = pn.givDosis(dato);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void givDosis_BeforeStartDate_ReturnsFalse()
        {
            var pn = new PN(DateTime.Today, DateTime.Today.AddDays(5), 1.0, new Laegemiddel());
            var dato = new Dato { dato = DateTime.Today.AddDays(-1) };

            var result = pn.givDosis(dato);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void doegnDosis_NoDosages_ReturnsZero()
        {
            var pn = new PN(DateTime.Today, DateTime.Today.AddDays(5), 1.0, new Laegemiddel());

            var result = pn.doegnDosis();

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void doegnDosis_TwoDosagesOverTwoDays_ReturnsCorrectValue()
        {
            var pn = new PN(DateTime.Today, DateTime.Today.AddDays(5), 2.0, new Laegemiddel());
            pn.givDosis(new Dato { dato = DateTime.Today });
            pn.givDosis(new Dato { dato = DateTime.Today.AddDays(1) });

            var result = pn.doegnDosis();

            // samletDosis = 2 * 2 = 4, dage = 2 → 4 / 2 = 2.0
            Assert.AreEqual(2.0, result);
        }
    }
}
