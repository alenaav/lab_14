using Microsoft.VisualStudio.TestTools.UnitTesting;
using lab14_2;
using lab12_4;
using System.Collections.Generic;
using AutomobileLibrary;

namespace LabTests
{
    [TestClass]
    public class ProgramTests
    {
        private MyCollection<Automobile> autos;

        [TestInitialize]
        public void Setup()
        {
            autos = new MyCollection<Automobile>(50);
            for (int i = 0; i < 10; i++)
            {
                autos.Add(Program.GenerateAuto());
            }
        }

        [TestMethod]
        public void TestGenerateAuto()
        {
            Automobile auto = Program.GenerateAuto();
            Assert.IsNotNull(auto);
            Assert.IsInstanceOfType(auto, typeof(Automobile));
        }

        [TestMethod]
        public void TestGetAutomobilesByBrandAndPriceExt()
        {
            var result = Program.GetAutomobilesByBrandAndPriceExt(autos, "SomeBrand", 20000);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestFindCarsByPriceAndBrandLinq()
        {
            var result = Program.FindCarsByPriceAndBrandLinq(autos, "SomeBrand", 20000);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCountCarsBySeatsExt()
        {
            int result = Program.CountCarsBySeatsExt(autos, 4);
            Assert.IsTrue(result >= 0);
        }

        [TestMethod]
        public void TestCountCarsBySeatsLinq()
        {
            int result = Program.CountCarsBySeatsLinq(autos, 4);
            Assert.IsTrue(result >= 0);
        }

        //
        
        //

        [TestMethod]
        public void TestGetMostExpensiveCarByYearExt()
        {
            var result = Program.GetMostExpensiveCarByYearExt(autos);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetMostExpensiveCarByYearLinq()
        {
            var result = Program.GetMostExpensiveCarByYearLinq(autos);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDisplayDictionary()
        {
            var dictionary = new Dictionary<int, Automobile>
            {
                { 2020, new Car { Brand = "BrandA", Price = 25000, Year = 2020 } }
            };

            // Capture console output
            using (var sw = new System.IO.StringWriter())
            {
                System.Console.SetOut(sw);
                Program.DisplayDictionary(dictionary);
                var result = sw.ToString().Trim();
                Assert.IsFalse(string.IsNullOrEmpty(result));
            }
        }
    }
}