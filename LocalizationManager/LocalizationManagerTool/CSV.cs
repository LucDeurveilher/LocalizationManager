using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        private DataTable dataTable;
        private void ImportCsv()
        {
            // Initialize the DataTable
            dataTable = new DataTable();
            dataGrid.Columns.Clear();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Nettoyer la DataTable avant de la remplir
                    dataTable.Clear();

                    string line = sr.ReadLine();
                    // Lire les en-têtes
                    if (line != null)
                    {
                        string[] headers = line.Split(';');

                        foreach (var header in headers)
                        {
                            if (!dataTable.Columns.Contains(header))
                            {
                                dataTable.Columns.Add(header);
                            }
                        }

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
                    }
                    else
                    {
                        // Gestion du cas où la ligne est null, par exemple en affichant un message d'erreur
                        Console.WriteLine("La ligne est vide ou null.");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'importation du fichier : {ex.Message}");
            }

            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void ExportCsv(string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    // Write headers
                    string headerLine = string.Join(";", dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName));
                    sw.WriteLine(headerLine);

                    // Write data
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string rowLine = string.Join(";", row.ItemArray.Select(item => item.ToString()));
                        sw.WriteLine(rowLine);
                    }
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
