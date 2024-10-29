using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LocalizationManagerTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> Columns = new List<string>();
        string filePath;
        public MainWindow()
        {
            InitializeComponent();
            Columns.Add("id");
            Columns.Add("en");
            Columns.Add("fr");
            Columns.Add("es");
            Columns.Add("ja");

            foreach (string column in Columns)
            {
                //Pour ajouter une colonne à notre datagrid
                DataGridTextColumn textColumn = new DataGridTextColumn();
                textColumn.Header = column;
                textColumn.Binding = new Binding(column);
                dataGrid.Columns.Add(textColumn);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers XML (*.xml)|*.xml|Fichiers CSV (*.csv)|*.csv|Fichiers JSON (*.json)|*.json|Tous les fichiers (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                MessageBox.Show("Fichier sélectionné : " + filePath);

                string extension = System.IO.Path.GetExtension(filePath);
                if (extension == ".xml")
                {
                    ImportXML();
                }
                else if (extension == ".csv")
                {
                    ImportCsv();
                }
                else if (extension == ".json")
                {
                    ImportJson();
;
                }
                else
                {
                    MessageBox.Show("Format de fichier inconnu.");
                }

            }
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {

        }
    }


}