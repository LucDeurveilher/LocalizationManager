using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {

        private enum LANGUAGE
        {
            ID,
            FR,
            EN,
            JP
        }
        public void ImportXML(string filePath)
        {
            List<Word> words = DeserializeFromXml(filePath);

            foreach (Word word in words)
            {
                DataRow row = dataTable.NewRow();

                row[(int)LANGUAGE.FR] = word.FrFR;
                row[(int)LANGUAGE.EN] = word.EnUS;
                row[(int)LANGUAGE.JP] = word.JaJP;

                dataTable.Rows.Add(row);
            }
        }

        private List<Word> DeserializeFromXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Words));
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                Words words = (Words)serializer.Deserialize(fs);
                return words.WordList;
            }
        }



        public void ExportXML(string filePath)
        {
            List<Word> words = new List<Word>();

            foreach (DataRow row in dataTable.Rows)
            {
                string rowLine = string.Join(";", row.ItemArray.Select(item => item.ToString()));
                Word word = new Word();

                word.EnUS = rowLine.Split(';')[(int)LANGUAGE.FR];
                word.FrFR = rowLine.Split(';')[(int)LANGUAGE.EN];
                word.JaJP = rowLine.Split(';')[(int)LANGUAGE.JP];
                words.Add(word);
            }

            SerializeToXml(words, filePath);
        }

        public static void SerializeToXml(List<Word> words, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Word>), new XmlRootAttribute("Words"));

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fileStream, words);
            }

            Console.WriteLine($"Le fichier XML a été créé : {filePath}");
        }

    }

    [XmlRoot("Words")]
    public class Words
    {
        [XmlElement("Word")]
        public List<Word> WordList { get; set; }
    }

}
