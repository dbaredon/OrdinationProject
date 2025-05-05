using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using shared.Model;

namespace ordination_test
{
    [TestClass]
    public class PNTests
    {
        [TestMethod] // TC1
        public void givDosis_OnStartDate_ReturnsTrue()
        {
            var pn = new PN(DateTime.Today, DateTime.Today.AddDays(5), 1.0, new Laegemiddel());
            var dato = new Dato { dato = DateTime.Today };

            var result = pn.givDosis(dato);

            Assert.IsTrue(result);
        }

        [TestMethod] // TC2
        public void givDosis_OnEndDate_ReturnsTrue()
        {
            var start = DateTime.Today;
            var end = DateTime.Today.AddDays(5);
            var pn = new PN(start, end, 1.0, new Laegemiddel());
            var dato = new Dato { dato = end };

            var result = pn.givDosis(dato);

            Assert.IsTrue(result);
        }

        [TestMethod] // TC3
        public void givDosis_BeforeStartDate_ReturnsFalse()
        {
            var pn = new PN(DateTime.Today, DateTime.Today.AddDays(5), 1.0, new Laegemiddel());
            var dato = new Dato { dato = DateTime.Today.AddDays(-1) };

            var result = pn.givDosis(dato);

            Assert.IsFalse(result);
        }

        [TestMethod] // TC4
        public void givDosis_AfterEndDate_ReturnsFalse()
        {
            var pn = new PN(DateTime.Today, DateTime.Today.AddDays(5), 1.0, new Laegemiddel());
            var dato = new Dato { dato = DateTime.Today.AddDays(6) };

            var result = pn.givDosis(dato);

            Assert.IsFalse(result);
        }

        [TestMethod] // TC5
        public void doegnDosis_NoDosages_ReturnsZero()
        {
            var pn = new PN(DateTime.Today, DateTime.Today.AddDays(5), 1.0, new Laegemiddel());

            var result = pn.doegnDosis();

            Assert.AreEqual(0, result);
        }

        [TestMethod] // TC6
        public void doegnDosis_TwoDosagesOverTwoDays_ReturnsCorrectValue()
        {
            var pn = new PN(DateTime.Today, DateTime.Today.AddDays(5), 2.0, new Laegemiddel());
            pn.givDosis(new Dato { dato = DateTime.Today });
            pn.givDosis(new Dato { dato = DateTime.Today.AddDays(1) });

            var result = pn.doegnDosis();

            // 2 doser á 2.0 → total 4.0 / 2 dage = 2.0
            Assert.AreEqual(2.0, result);
        }
    }
}
