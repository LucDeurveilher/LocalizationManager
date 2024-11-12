using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Reflection.PortableExecutable;
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
            EN,
            FR,
            JP
        }
        public void ImportXML(string filePath)
        {
            XDocument xdoc = XDocument.Load(filePath);
            var words = xdoc.Descendants("Word");
            dataTable.Clear();


            foreach (var word in words)
            {
                DataRow dataRow = dataTable.NewRow();

                string id = word.Element("Id")?.Value;
                string enUS = word.Element("EnUS")?.Value;
                string frFR = word.Element("FrFR")?.Value;
                string jaJP = word.Element("JaJP")?.Value;

                Console.WriteLine($"EN: {enUS}, FR: {frFR}, JP: {jaJP}");

                dataRow["id"] = id;
                dataRow["en"] = enUS;
                dataRow["fr"] = frFR;
                dataRow["jp"] = jaJP;

                dataTable.Rows.Add(dataRow);
            }
            dataGrid.ItemsSource = dataTable.DefaultView;
        }


        public void ExportXML(string filePath)
        {
            List<Word> words = new List<Word>();

            foreach (DataRow row in dataTable.Rows)
            {
                string rowLine = string.Join(";", row.ItemArray.Select(item => item.ToString()));
                Word word = new Word();

                word.Id = rowLine.Split(';')[(int)LANGUAGE.ID];
                word.EnUS = rowLine.Split(';')[(int)LANGUAGE.EN];
                word.FrFR = rowLine.Split(';')[(int)LANGUAGE.FR];
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

}
