using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVTesting
{
    public class CSVProcess : IProcess<CSVData>
    {
        private string _colName;
        private Decimal _percentage;
        private string _filePath;
        public CSVProcess(string colName, Decimal percentage, string filePath)
        {
            _colName = colName;
            _percentage = percentage;
            _filePath = filePath;
        }
        /// <summary>
        /// Process file.
        /// </summary>
        /// <param name="csvData"></param>
        public void Process(CSVData csvData)
        {
            var listToFindMedian = csvData.GetColValues<Decimal>(_colName);
            //Find Median
            var finalMedian = Helper.Median(listToFindMedian);
            IFilter<CSVData> filter = new DeviationPercentage(finalMedian, _percentage, _colName);
            //Filter data on percentage
            CSVData filteredData = filter.Filter(csvData);
            //Print output
            var fileName = Helper.ExtractFileName(_filePath);
            IOutput<CSVData> csvOutput = new CSVOutput(finalMedian, _colName, fileName);
            csvOutput.Output(filteredData);

        }

    }
}
