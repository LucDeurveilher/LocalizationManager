using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Specialized;
using System.Windows;
using System.Text.Json.Serialization;
using System.Data;
using System.Windows.Controls;
namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        void ImportJson(string filePath)
        {
            // Charger le fichier JSON et le désérialiser
            string json = File.ReadAllText(filePath);
            List<Dictionary<string, string>> words = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);

            if (words == null || words.Count == 0)
            {
                MessageBox.Show("Le fichier JSON est vide ou invalide.");
                return;
            }

            // Effacer la DataTable et configurer les colonnes
            dataTable.Clear();
            dataTable.Columns.Clear();

            // Ajouter les colonnes dynamiquement en fonction des clés du premier objet
            foreach (var key in words[0].Keys)
            {
                if (!dataTable.Columns.Contains(key))
                {
                    dataTable.Columns.Add(key);
                }
            }

            // Ajouter les lignes dans le DataTable
            foreach (var word in words)
            {
                DataRow dataRow = dataTable.NewRow();

                foreach (var key in word.Keys)
                {
                    dataRow[key] = word[key]; // Remplir chaque cellule avec la valeur correspondante
                }

                dataTable.Rows.Add(dataRow);
            }

            // Mettre à jour la source de données du DataGrid
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        public void ExportJson(string filePath)
        {
            try
            {
                // Convertir le DataTable en une liste de dictionnaires
                List<Dictionary<string, string>> words = new List<Dictionary<string, string>>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Dictionary<string, string> word = new Dictionary<string, string>();

                    foreach (DataColumn column in dataTable.Columns)
                    {
                        string columnName = column.ColumnName;
                        string cellValue = row[column]?.ToString() ?? string.Empty; // Gérer les valeurs nulles
                        word.Add(columnName, cellValue);
                    }

                    words.Add(word);
                }

                // Sérialiser la liste en JSON
                string json = JsonConvert.SerializeObject(words, Formatting.Indented);

                // Écrire le JSON dans le fichier
                File.WriteAllText(filePath, json);

                MessageBox.Show("Exportation réussie !");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'exportation : {ex.Message}");
            }
        }
    }

}

[JsonObject(MemberSerialization.OptIn)]
public class Word
{

    [JsonProperty]
    public string Id { get; set; }
    [JsonProperty]
    public Dictionary<string, string> words { get; set; }
}