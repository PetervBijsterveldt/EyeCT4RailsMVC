using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EyeCT4RailzMVC.Models;

namespace EyeCT4RailzMVC.Tests.ModelTests
{
    [TestClass]
    public class TramTests
    {
        private Tram tram_1;
        private Tram tram_2;
        private Tram tram_3;
        private Tram tram_4;

        private TramType type_1;
        private TramType type_2;
        private TramType type_3;

        private SchoonmaakBeurt SBeurt;
        private ReparatieBeurt RBeurt;

        [TestInitialize]
        public void TestInitialize()
        {
            tram_1 = new Tram(0, 0, type_1, 10, 10, TramStatus.Dienst, /* vervuild */ false, /* defect */ false, /* bestuurder geschikt */ true, /* beschikbaar */ false);
            tram_2 = new Tram(1, 0, type_2, 11, 15, TramStatus.Onderhoud, false, true, false, false);
            tram_3 = new Tram(2, 0, type_3, 12, 5, TramStatus.Remise, false, false, true, true);
            tram_4 = tram_3;

            type_1 = TramType.Combino;
            type_2 = TramType.Opleidingtram;
            type_3 = TramType._11G;

            SBeurt = new SchoonmaakBeurt("Piet", DateTime.Now, SchoonmaakType.SchoonmaakGroot);
            RBeurt = new ReparatieBeurt(0, "Jan", 1, DateTime.Now, ReparatiebeurtType.ReparatieGroot, 0);
        }
        [TestMethod]
        public void ConstructorTest()
        {
            Assert.AreNotEqual(tram_1, tram_2, "tram_1 en tram_2 zijn niet hetzelfde.");
            Assert.AreEqual(tram_3, tram_4, "Dit zijn 2 dezelfde objecten.");
        }

        [TestMethod]
        public void StatusVeranderen()
        {
            tram_3.StatusVeranderen(TramStatus.Dienst);

            Assert.AreEqual(tram_3.Status, TramStatus.Dienst);
        }

        [TestMethod]
        public void EditTram()
        {
            tram_4.EditTram(13, 13, TramStatus.Dienst, type_3);

            Assert.AreNotSame(tram_3.TramNr, tram_4.TramNr);
        }

        [TestMethod]
        public void AddSchoonmaakEnReparatie()
        {
            tram_3.Schoonmaak(SBeurt);
            tram_3.Reparatie(RBeurt);

            Assert.AreEqual(1, tram_3.SchoonmaakBeurten.Count);
            Assert.AreEqual(1, tram_3.ReparatieBeurten.Count);
        }
    }
}
