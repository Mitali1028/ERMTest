using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {                
               Console.WriteLine("Please enter a FilePath:");
               string filePath = Console.ReadLine();                

               //string filePath = ConfigurationManager.AppSettings.Get("CSVFilePath");
                //Createing CSV Parser for csvFile.
                var csvParser = new CSVParser();
                var csvFileData = csvParser.ParseFile(filePath);

                //Createing LPProcess. This can be moved to factory class or improved in other ways
                if (filePath.ToLower().Contains("lp"))
                {
                    string medianColumnName = Helper.getConfigProperty<String>("LP.medianColumn");
                    decimal filterPercent = Helper.getConfigProperty<decimal>("LP.filterPercent");
                    IProcess<CSVData> LPProcess = new CSVProcess(medianColumnName, filterPercent,filePath);
                    LPProcess.Process(csvFileData);
                }
                //creating TOUProcess. Obvious duplication of code here from above if. As mentioned can be improved.
                //I have not optimized it for now. Design can be guided by future requirement change
                if (filePath.ToLower().Contains("tou"))
                {
                    string medianColumnName = Helper.getConfigProperty<String>("TOU.medianColumn");
                    decimal filterPercent = Helper.getConfigProperty<decimal>("TOU.filterPercent");
                    IProcess<CSVData> touProcess = new CSVProcess(medianColumnName, filterPercent, filePath);
                    touProcess.Process(csvFileData);
                }
      
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }           
        }
    }      
}
