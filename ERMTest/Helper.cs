using System;
using System.Collections.Generic;
using System.Configuration;

namespace CSVTesting
{
    public static class Helper
    {    
        // Function to extract property from file.
        public static T getConfigProperty<T>(string propertyName)
        {
            string propertyValue = ConfigurationManager.AppSettings.Get(propertyName);
            if(propertyValue == null)
            {
                throw new Exception("Property " + propertyName + " is not configured");
            }
            T value = (T)Convert.ChangeType(propertyValue, typeof(T));
            return value;
        }
        public static decimal Median(List<decimal> Values)
        {
            if (Values.Count == 0)
                return 0.0m;
            Values.Sort();
            if (Values.Count % 2 == 0)
            {
                return (Values[(Values.Count / 2)] + Values[(Values.Count / 2) - 1]) / 2;
            }
            else
            {
                return Values[(Values.Count / 2)];
            }
        }

        public static string ExtractFileName(string path)
        {
            string fileName = string.Empty;
            int lastIndex = path.LastIndexOf("\\");
            if (lastIndex >= 0)
            {
                fileName = path.Substring(lastIndex + 1);
            }
            return fileName;
        }
    }
}
