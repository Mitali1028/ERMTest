using System;
using System.Collections.Generic;

namespace CSVTesting
{
   public class CSVData
    {
        public List<string[]> Rows { get; set; }
        public Dictionary<string,int> ColHeaderDictionary { get; set; }
       /// <summary>
       /// Get specific column values based on its name.
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="colName"></param>
       /// <returns></returns>
        public List<T> GetColValues<T>(string colName)
        {          
            int colIndex = GetColumnIndex(colName);
            var ListForMedian = new List<T>();
           
                foreach (var row in Rows)
                {
                    if (row.Length >= colIndex)
                    {
                        string colForMedian = row[colIndex]; // DataValue and Energy column for csv
                        T value = (T)Convert.ChangeType(colForMedian, typeof(T));

                        ListForMedian.Add(value);
                    }
                }            
            return ListForMedian;
        }

        /// <summary>
        /// Get the index of the given Column
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public int GetColumnIndex(String colName) {

            int colIndex = 0;           
            if (!ColHeaderDictionary.TryGetValue(colName, out colIndex))
            {
                throw new Exception("Column " + colName + " does not exist in csv data");
            }
            return colIndex;
        }


    }
}
