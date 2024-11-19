using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {

        public void ImportXML(string filePath)
        {
            XDocument xdoc = XDocument.Load(filePath);
            var words = xdoc.Descendants("Word");

            List<string> tagNames = words.Elements().Select(element => element.Name.LocalName).ToList();

            dataTable.Clear();

            foreach (var tagName in tagNames)
            {
                if (!dataTable.Columns.Contains(tagName))
                {
                    dataTable.Columns.Add(tagName);
                }

            }

            foreach (var word in words)
            {
                DataRow dataRow = dataTable.NewRow();

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    string id = word.Element(dataTable.Columns[i].ToString())?.Value;

                    dataRow[dataTable.Columns[i].ToString()] = id;

                }

                dataTable.Rows.Add(dataRow);
            }

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = dataTable.DefaultView;
        }


        public void ExportXML(string filePath)
        {
            List<Word> words = new List<Word>();

            foreach (DataRow row in dataTable.Rows)
            {
                string rowLine = string.Join(";", row.ItemArray.Select(item => item.ToString()));

                Word word = new Word();

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    DataColumn column = dataTable.Columns[i];

                    string columName = column.ToString();

                    if(rowLine.Split(';')[i] != "")
                    {
                        word.words.Add(columName, rowLine.Split(';')[i]);
                    }

                }


                words.Add(word);

            }

            SerializeToXml(words, filePath);
        }

        public static void SerializeToXml(List<Word> words, string filePath)
        {
            // Création de l'élément racine
            XElement root = new XElement("Words");

                foreach (var word in words)
                {
                    // Création de l'élément <Word> pour chaque mot
                    XElement wordElement = new XElement("Word");

                    foreach (var kvp in word.words)
                    {
                        // Ajouter chaque traduction comme élément avec nom de la langue et texte
                        XElement translationElement = new XElement(kvp.Key, kvp.Value);
                        wordElement.Add(translationElement);
                    }

                    root.Add(wordElement);
                }

            // Sauvegarde dans le fichier
            XDocument document = new XDocument(root);
            document.Save(filePath);

            Console.WriteLine($"Le fichier XML a été créé : {filePath}");
        }



    }

}
