using System;
using System.Collections.Generic;

namespace CSVTesting
{
   public class DeviationPercentage : IFilter<CSVData>
    {

        private Decimal _median;
        private Decimal _percentdev;
        private String _columnName;
        public DeviationPercentage(Decimal median, Decimal percentdev, String columnName ) {
            this._median = median;
            this._percentdev = percentdev;
            this._columnName = columnName;
        }
        /// <summary>
        /// Filter the data based on given percentage
        /// </summary>
        /// <param name="csvData"></param>
        /// <returns></returns>
        public CSVData Filter(CSVData csvData)
        {
            CSVData filteredData = new CSVData();
            filteredData.ColHeaderDictionary = csvData.ColHeaderDictionary;
            List<String[]> filteredRows = new List<string[]>();
            filteredData.Rows = filteredRows;
            int datavalueIndex = csvData.GetColumnIndex(_columnName);
            foreach (string[] line in csvData.Rows)
            {             
                string colForMedian = line[datavalueIndex];
                decimal colValDecimal = 0.0m;
                Decimal.TryParse(colForMedian, out colValDecimal);
                var percentage = (_median * _percentdev) / 100;
                var lowerDev = _median - percentage;
                var upperDev = _median + percentage;
                if (colValDecimal < lowerDev || colValDecimal > upperDev)
                {
                    filteredRows.Add(line);
                }
            }
            return filteredData;
        }
    }
}
