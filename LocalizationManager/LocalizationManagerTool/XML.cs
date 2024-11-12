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
        private void ImportXML()
        {

        }

        public void ExportXML(string filePath)
        {
            List<Word> words = new List<Word>();

            for (int i = 0; i < 3; i++)
            {
                Word word = new Word
                {
                    EnUS = "Poop",
                    FrFR = "Caca",
                    JaJP = "Cacaligato"
                };
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


}
