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

            
            string json = File.ReadAllText(filePath);
            List<Word> translation = JsonConvert.DeserializeObject<List<Word>>(json);
            
            foreach (Word word in translation)
            {
                DataRow row = dataTable.NewRow();
                row["ID"] = word.Id;
                row["EN"] = word.EnUS;
                row["FR"] = word.FrFR;
                row["JP"] = word.JaJP;
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
                word.Id = row.ItemArray.GetValue(0).ToString() ?? "";
                word.EnUS = row.ItemArray.GetValue(1).ToString() ?? "";
                word.FrFR = row.ItemArray.GetValue(2).ToString() ?? "";
                word.EsES = row.ItemArray.GetValue(3).ToString() ?? "";
                word.JaJP = row.ItemArray.GetValue(4).ToString() ?? "";
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

public class Translation
{
    public Dictionary<int,Word> words = new Dictionary<int, Word>();
}
