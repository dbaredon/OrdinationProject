using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using shared.Model;

namespace ordination_test
{
    [TestClass]
    public class DagligSkaevTests
    {
        private DagligSkæv lavDagligSkaevMedDatoer(DateTime start, DateTime slut, Laegemiddel lm)
        {
            return new DagligSkæv(start, slut, lm);
        }

        [TestMethod] // TC1
        public void opretDosis_ValidTime_AddsCorrectDose1()
        {
            var ds = lavDagligSkaevMedDatoer(DateTime.Today, DateTime.Today.AddDays(2), new Laegemiddel());
            ds.opretDosis(new DateTime(1, 1, 1, 9, 0, 0), 2);

            Assert.AreEqual(1, ds.doser.Count);
            Assert.AreEqual(2, ds.doser[0].antal);
        }

        [TestMethod] // TC2
        public void opretDosis_ValidTime_AddsCorrectDose2()
        {
            var ds = lavDagligSkaevMedDatoer(DateTime.Today, DateTime.Today.AddDays(2), new Laegemiddel());
            ds.opretDosis(new DateTime(1, 1, 1, 14, 0, 0), 3);

            Assert.AreEqual(1, ds.doser.Count);
            Assert.AreEqual(3, ds.doser[0].antal);
        }

        [TestMethod] // TC3
        public void doegnDosis_WithTwoDoses_ReturnsSum()
        {
            var ds = lavDagligSkaevMedDatoer(DateTime.Today, DateTime.Today.AddDays(2), new Laegemiddel());
            ds.opretDosis(new DateTime(1, 1, 1, 9, 0, 0), 2);
            ds.opretDosis(new DateTime(1, 1, 1, 14, 0, 0), 3);

            var result = ds.doegnDosis();

            Assert.AreEqual(5, result);
        }

        [TestMethod] // TC4
        public void samletDosis_WithMultipleDaysAndDoses_ReturnsTotal()
        {
            var ds = lavDagligSkaevMedDatoer(new DateTime(2025, 5, 1), new DateTime(2025, 5, 3), new Laegemiddel());
            ds.opretDosis(new DateTime(1, 1, 1, 9, 0, 0), 2);
            ds.opretDosis(new DateTime(1, 1, 1, 14, 0, 0), 3);

            var result = ds.samletDosis(); // 5 enheder * 3 dage = 15

            Assert.AreEqual(15, result);
        }

        [TestMethod] // TC5
        public void opretDosis_NegativeDoseValue_StillAdded()
        {
            var ds = lavDagligSkaevMedDatoer(DateTime.Today, DateTime.Today, new Laegemiddel());
            ds.opretDosis(new DateTime(1, 1, 1, 9, 0, 0), -1);

            Assert.AreEqual(1, ds.doser.Count);
            Assert.AreEqual(-1, ds.doser[0].antal); 
        }

        [TestMethod] // TC6
        public void doegnDosis_WithoutDoses_ReturnsZero()
        {
            var ds = lavDagligSkaevMedDatoer(DateTime.Today, DateTime.Today.AddDays(1), new Laegemiddel());

            var result = ds.doegnDosis();

            Assert.AreEqual(0, result);
        }

        [TestMethod] // TC7
        public void samletDosis_WithoutDoses_ReturnsZero()
        {
            var ds = lavDagligSkaevMedDatoer(DateTime.Today, DateTime.Today.AddDays(1), new Laegemiddel());

            var result = ds.samletDosis();

            Assert.AreEqual(0, result);
        }
    }
}
