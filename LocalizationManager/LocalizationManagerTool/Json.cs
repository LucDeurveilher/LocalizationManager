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
namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        private void ImportJson()
        {

            
            MessageBox.Show("La fonction est appelée !");
            string json = File.ReadAllText(filePath);
            DataTable dataTable = new DataTable();
            List<Word> translation = JsonConvert.DeserializeObject<List<Word>>(json);
            foreach (var translationItem in translation[0].words.Keys)
            {
                Columns.Add(translationItem);
            }
            foreach (Word word in translation)
            {
               
                    DataRow row = dataTable.NewRow();
                    dataTable.Rows.Add(row);
            }

            dataGrid.ItemsSource = dataTable.DefaultView;
        }
        void ExportJson(string filePath)
        {
            List<Word> words = new List<Word>();
            foreach (DataRow row in dataTable.Rows)
            {
                Word word = new Word();
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    DataColumn column = dataTable.Columns[i];
                    string name = column.ToString();
                    word.words.Add(name, row.ItemArray.GetValue(i).ToString());

                }

                words.Add(word);
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(words);
            StreamWriter sw = new StreamWriter(filePath, false);
            sw.Write(json);
            sw.Close();
        }
    }

}

[JsonObject(MemberSerialization.OptIn)]
public class Word
{
    [JsonProperty]
    public Dictionary<string, string> words { get; set; }
}
