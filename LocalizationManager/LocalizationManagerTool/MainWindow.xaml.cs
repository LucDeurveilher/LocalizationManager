using Microsoft.Win32;
using System.Data;
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
            dataTable = new DataTable();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("EN");
            dataTable.Columns.Add("FR");
            dataTable.Columns.Add("JP");
            dataGrid.ItemsSource = dataTable.DefaultView;

        }

        private void ImportMenuItem_Click(object sender, RoutedEventArgs e)
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
                    ImportXML(filePath);
                }
                else if (extension == ".csv")
                {
                    ImportCsv();
                }
                else if (extension == ".json")
                {
                    ImportJson();
                }
                else
                {
                    MessageBox.Show("Format de fichier inconnu.");
                }

            }
        }

        private void ExportMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Fichiers XML (*.xml)|*.xml|Fichiers CSV (*.csv)|*.csv|Fichiers JSON (*.json)|*.json";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                MessageBox.Show("Emplacement de sauvegarde : " + filePath);

                string extension = System.IO.Path.GetExtension(filePath);

                if (extension == ".xml")
                {
                    ExportXML(filePath);
                }
                else if (extension == ".csv")
                {
                    ExportCsv(filePath);
                }
                else if (extension == ".json")
                {
                    ExportJson(filePath);
                }
                else
                {
                    MessageBox.Show("Format de fichier inconnu.");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {

        }
    }


}