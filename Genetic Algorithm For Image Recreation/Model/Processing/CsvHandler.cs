using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm_For_Image_Recreation.Model.Processing
{
    static class CsvHandler
    {
        public static void WriteToCsv(List<CsvData> csvDatas)
        {

            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = $"Result_{DateTime.Now.ToString("dd_MM_yyyy-HH-mm-ss")}.csv";
            string fullPath = Path.Combine(folderPath, fileName);

            var configCsv = new CsvConfiguration(new CultureInfo("pl-PL"))
            {
                Delimiter = ";"
            };


            using (var writer = new StreamWriter(fullPath))
            using (var csv = new CsvWriter(writer, configCsv))
            {
                csv.WriteRecords(csvDatas);
            }
        }
    }
}
