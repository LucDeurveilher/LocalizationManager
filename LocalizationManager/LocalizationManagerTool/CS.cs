using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {

        private void ExportScriptCS(string filePath)
        {
            StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);

            string nameClass = "public class Localization \n" +
                    "{\n" +
                    "   \n" +

                    "}";


            sw.WriteLine(nameClass);


            using (StreamReader sr = new StreamReader(filePath))
            {

                string line = sr.ReadLine();
                // Lire les en-têtes
                if (line != null)
                {
                    string[] headers = line.Split(';');

                    foreach (var header in headers)
                    {
                        sw.WriteLine(ReturnFonction(header));
                    }

                    /*
                    // Lire les lignes du fichier CSV
                    while (!sr.EndOfStream)
                    {
                        string[] rows = sr.ReadLine().Split(';');
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dataRow[i] = rows.Length > i ? rows[i] : string.Empty;
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                    */
                }
                MessageBox.Show("Exportation réussie!");
            }



        }

        public string ReturnFonction(string _name)
        {
            string FonctionName = $"static string Get{_name}(string _languageCode) \n" + "{}";

            return FonctionName;
        }
    }
}


