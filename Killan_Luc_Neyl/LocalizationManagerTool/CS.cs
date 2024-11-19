using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {

        private void ExportScriptCS(string filePath)
        {

            string nameClass = "public class Localization \n" +
                "{\n" +
                "\n";

            using (StreamWriter sw = new StreamWriter(filePath))
            {

                sw.WriteLine(nameClass);

                int column = 0;

                foreach (DataRow row in dataTable.Rows)
                {
                    sw.WriteLine(ReturnFonction(row[0].ToString(), column));
                    column++;
                }

                sw.WriteLine("\n }");
            }

        }

        public string ReturnFonction(string _name, int column)
        {
            string FonctionName = $"static string Get{_name}(string _languageCode) \n" + "{";
            int count = 0;
            foreach (var header in dataTable.Columns)
            {
                //skip ID
                if (count == 0)
                {
                    count++;
                    continue;
                }

                FonctionName += $"if (_languageCode == \"{header}\") \n" + "{";

                FonctionName += $"return \"{dataTable.Rows[column][count]}\"; "+"\n }";

                count++;
            }

           
            FonctionName += "}";
            return FonctionName;
        }
    }
}


