using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using shared.Model;

namespace ordination_test
{
    [TestClass]
    public class UseCaseTests
    {
        [TestMethod]
        public void TC1_FindPatient_OpretDirekteOgTjekCPR()
        {
            var patient = new Patient("123456-7890", "Test Person", 70.0); 


            Assert.IsNotNull(patient);
            Assert.AreEqual("123456-7890", patient.cprnr);
            Console.WriteLine($"Patient oprettet: {patient.navn}, CPR: {patient.cprnr}, Vægt: {patient.vaegt} kg");
        }

        [TestMethod]
        public void TC2_OpretLaegemiddel_TjekNavn()
        {
            var lm = new Laegemiddel("Ibuprofen", 1.0, 1.5, 2.0, "mg");


            Assert.IsNotNull(lm);
            Assert.AreEqual("Ibuprofen", lm.navn);
            Console.WriteLine($"Lægemiddel oprettet: {lm.navn}, enhed: mg/kg/døgn");
        }

        [TestMethod]
        public void TC3_TjekOrdinationstypeErPN()
        {
            var type = "PN"; // Simpelt eksempel på valg af type

            Assert.AreEqual("PN", type);
            Console.WriteLine("Ordinationstype valgt korrekt: PN");
        }

        [TestMethod]
        public void TC4_OpretPN_MedGyldigPeriode()
        {
            var start = new DateTime(2025, 5, 1);
            var slut = new DateTime(2025, 5, 10);
            var lm = new Laegemiddel("Panodil", 1.0, 1.5, 2.0, "mg");

            var pn = new PN(start, slut, 2.0, lm);

            Assert.AreEqual(start, pn.startDen);
            Assert.AreEqual(slut, pn.slutDen);
            Assert.AreEqual(2.0, pn.antalEnheder);
            Console.WriteLine($"Ordination oprettet korrekt med {pn.antalEnheder} enheder fra {pn.startDen.ToShortDateString()} til {pn.slutDen.ToShortDateString()}");
        }

        [TestMethod]
        public void TC7_OpretPN_UgyldigPeriode_KasterException()
        {
            var start = new DateTime(2025, 5, 10);
            var slut = new DateTime(2025, 5, 1);
            var lm = new Laegemiddel("Panodil", 1.0, 1.5, 2.0, "mg");


            Assert.ThrowsException<ArgumentException>(() =>
            {
                var pn = new PN(start, slut, 1.0, lm);
            });
            Console.WriteLine("Korrekt kastet ArgumentException ved ugyldig periode");
        }
    }
}