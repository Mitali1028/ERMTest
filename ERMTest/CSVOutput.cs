using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVTesting
{
    public class CSVOutput : IOutput<CSVData>
    {
        //instead of hardcoding outputcol date/time pass it in list of columnnames
        const string OUTPUTCOLFORDATE = "date/time";
        private Decimal _median; 
        private string _medianColName;
        private string _fileName;

        //this should be enhanced to take list of columnnames required to be printed as argument and print them all
        public CSVOutput(Decimal median,string medianColName,string fileName)
        {
            this._median = median;
            this._medianColName = medianColName;
            this._fileName = fileName;
        }       
        /// <summary>
        /// Output result to the console.
        /// </summary>
        /// <param name="data"></param>
        public void Output(CSVData data)
        {
            string fileName = _fileName;
            int colIndex = data.GetColumnIndex(_medianColName);
            int dateColIndex = data.GetColumnIndex(OUTPUTCOLFORDATE);

            foreach (string[] line in data.Rows)
            {
                string  colValue =  line[colIndex];
                string dateTimeval =  line[dateColIndex];
                Console.WriteLine(string.Format("{0} {1} {2} {3}", fileName, dateTimeval, colValue, _median));
            }
        }
    }
}
