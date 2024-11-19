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

            string nameClass = "public class Localization \n" +
                    "{\n" +
                    "   \n" +
                    "}";
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    sw.WriteLine(nameClass);

                    /*
                    // Write data
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string rowLine = string.Join(";", row.ItemArray.Select(item => item.ToString()));
                        sw.WriteLine(rowLine);
                    }
                    */
                }
                MessageBox.Show("Exportation réussie!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'exportation : {ex.Message}");
            }

        }
    }
}

