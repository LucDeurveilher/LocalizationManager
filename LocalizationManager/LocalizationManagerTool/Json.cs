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
namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        private void ImportJson()
        {

            TestJson();
            //MessageBox.Show("La fonction est appelée !");
            //string json = File.ReadAllText(filePath);

            //Translation translation = JsonConvert.DeserializeObject<Translation>(json);
            //for (int i = 0; i < translation.words.Count; i++)
            //{
            //    MessageBox.Show(translation.words[i].FrFR + translation.words[i].EnUS + translation.words[i].JaJP);
            //}
        }
        void TestJson()
        {
            List<Word> words = new List<Word>();
            for (int i = 0; i < 3; i++)
            {
                Word word = new Word();
                word.EnUS = "A";
                word.FrFR = "A";
                word.JaJP = "A";
                words.Add(word);
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(words);
            StreamWriter sw = new StreamWriter("ToolTipOpening.txt",false);
            sw.Write(json);
            sw.Close();
        }
    }

}

[JsonObject(MemberSerialization.OptIn)]
public class Word
{
    [JsonProperty]
    public string EnUS { get; set; }
    [JsonProperty]
    public string FrFR { get; set; }
    [JsonProperty]
    public string JaJP { get; set; }
}

public class Translation
{
    public Dictionary<int,Word> words = new Dictionary<int, Word>();
}
