using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CSVTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSVTestProject
{
    [TestClass]
    public class CSVParserTest
    {
        const string MEDIANCOLUMN = "datavalue";
        Mock<CSVData> csvSampleData = new Mock<CSVData>();
        const string filePath = "..\\..\\CSVFiles\\LP_210095893_20150901T011608049.csv";

        private void SetDictionary(CSVData csvData)
        {
            csvData.ColHeaderDictionary = new Dictionary<string, int>();
            csvData.ColHeaderDictionary.Add(MEDIANCOLUMN, 5);
            csvData.ColHeaderDictionary.Add("date/time", 3);
        }

        //Intialize sample data for test
        [TestInitialize]
        public void SetCSVData()
        {

            SetCSVData(filePath);
        }

        private void SetCSVData(string filepath)
        {
            SetDictionary(csvSampleData.Object);
            SetRows(csvSampleData.Object,filepath);
        }
        private void SetRows(CSVData csvData, string filepath)
        {
            var csvParser = new Mock<CSVParser>() { CallBase = true };
            string[] csvfile = File.ReadAllLines(filepath);
            var fileData = csvfile.Skip(1);

            csvData.Rows = new List<string[]>();
            foreach (var line in fileData)
            {
                csvData.Rows.Add(line.Split(','));
            }

        }
        [TestMethod]
        public void CSVParser_Test_Positive()
        {
            var csvParser = new Mock<CSVParser>() { CallBase = true };           
            var csvData = csvParser.Object.ParseFile(filePath);
            Assert.IsNotNull(csvData);
            Assert.IsTrue(csvData.Rows.Count > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException), "file Path is wrong")]
        public void CSVParser_Test_Negative()
        {
            var csvParser = new Mock<CSVParser>() { CallBase = true };
            string missingFile = "..\\..\\LP_210095893_20150901T011608049.csv";
            var csvData = csvParser.Object.ParseFile(missingFile);

            Assert.IsNotNull(csvData);
        }

        [TestMethod]
        public void CSVData_GetValues_Test()
        {

            var colValues = csvSampleData.Object.GetColValues<Decimal>(MEDIANCOLUMN);
            Assert.IsNotNull(colValues);
            Assert.IsTrue(colValues.Count > 0);
        }

        [TestMethod]
        public void CSVProcess_Test()
        {

            Decimal percentage = 20;            
            var csvProcess = new Mock<CSVProcess>(MEDIANCOLUMN, percentage, filePath) { CallBase = true };
            csvProcess.Object.Process(csvSampleData.Object);


        }
        [TestMethod]
        public void CSVFilter_Test()
        {
            Decimal percentage = 20;
            Decimal median = 2.3m;
            var filter = new Mock<DeviationPercentage>(median, percentage, MEDIANCOLUMN) { CallBase = true };
            var csvObject = filter.Object.Filter(csvSampleData.Object);
            Assert.IsNotNull(csvObject);
            Assert.IsTrue(csvObject.Rows.Count > 0);
        }
        [TestMethod]
        public void CSVOutPut_Test()
        {
            string fileName = "LP_210095893_20150901T011608049.csv";
            Decimal median = 2.3m;
            var csvOutPut = new Mock<CSVOutput>(median, MEDIANCOLUMN, fileName) { CallBase = true };
            csvOutPut.Object.Output(csvSampleData.Object);            
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CSVOutPut_Test_column_not_available()
        {
            string fileName = "LP_210095893_20150901T011608049.csv";
            Decimal median = 2.3m;
            var csvOutPut = new Mock<CSVOutput>(median, "randomcolumn", fileName) { CallBase = true };
            csvOutPut.Object.Output(csvSampleData.Object);
        }

    }
}
