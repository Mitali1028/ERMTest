using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSVTesting
{
    public class CSVParser : IParser<CSVData>
    {
        /// <summary>
        /// parse file and return data object of file info.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public CSVData ParseFile(string filePath)
        {
            var fileRows = new List<string[]>();
            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            string[] csvfile = File.ReadAllLines(filePath);
            // getting header info
            var colHeadear = csvfile[0];
            var fileHeaderInfo = ExtractHeader(colHeadear);
            //getting fileData
            var fileData = csvfile.Skip(1);
            foreach (var line in fileData)
            {
                fileRows.Add(line.Split(','));
            }
            // Creating CSVDataObject to returnInfo
            var fileDataObject = new CSVData
            {
                ColHeaderDictionary = fileHeaderInfo,
                Rows = fileRows
            };

            return fileDataObject;
        }

        /// <summary>
        /// Make Dictionary of all headers and their indexes.
        /// </summary>
        /// <param name="headerLine"></param>
        /// <returns></returns>
        private Dictionary<string, int> ExtractHeader(String headerLine)
        {
            var fileHeaderInfo = new Dictionary<string, int>();

            int indexCounter = 0;
            string[] parts = headerLine.Split(',');
            foreach (var part in parts)
            {
                fileHeaderInfo.Add(part.ToLower().Replace(" ", ""), indexCounter);
                indexCounter++;
            }
            return fileHeaderInfo;
        }
    }
}
