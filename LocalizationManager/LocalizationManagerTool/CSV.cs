﻿using Microsoft.Win32;
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
        private DataTable dataTable;
        private void ImportCsv()
        {
            // Initialize the DataTable
            dataTable = new DataTable();
            dataGrid.ItemsSource = dataTable.DefaultView;

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Nettoyer la DataTable avant de la remplir
                    dataTable.Clear();

                    // Lire les en-têtes
                    string[] headers = sr.ReadLine().Split(';');
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'importation du fichier : {ex.Message}");
            }
        }

        private void ExportCsv()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Fichiers CSV (*.csv)|*.csv|Tous les fichiers (*.*)|*.*",
                FileName = "export.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
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


}
