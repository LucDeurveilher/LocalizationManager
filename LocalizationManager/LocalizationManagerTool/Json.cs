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
            List<Word> translation = JsonConvert.DeserializeObject<List<Word>>(json);
            dataTable.Columns.Add("EnUS", typeof(string));
            dataTable.Columns.Add("FrFR", typeof(string));
            dataTable.Columns.Add("EsES", typeof(string));
            dataTable.Columns.Add("JaJP", typeof(string));
            foreach (Word word in translation)
            {
                DataRow row = dataTable.NewRow();
                row["EnUS"] = word.EnUS;
                row["FrFR"] = word.FrFR;
                row["EsES"] = word.EsES;
                row["JaJP"] = word.JaJP;
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
                word.EnUS = row.ItemArray.GetValue(0).ToString() ?? "";
                word.FrFR = row.ItemArray.GetValue(1).ToString() ?? "";
                word.EsES = row.ItemArray.GetValue(2).ToString() ?? "";
                word.JaJP = row.ItemArray.GetValue(3).ToString() ?? "";
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
    public string Id { get; set; }
    [JsonProperty]
    public string EnUS { get; set; }
    [JsonProperty]
    public string FrFR { get; set; }

    [JsonProperty]
    public string EsES { get; set; }

    [JsonProperty]

    public string JaJP { get; set; }
}

public class Translation
{
    public Dictionary<int,Word> words = new Dictionary<int, Word>();
}
