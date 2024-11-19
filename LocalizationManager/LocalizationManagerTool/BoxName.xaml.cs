using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LocalizationManagerTool
{
    /// <summary>
    /// Interaction logic for BoxName.xaml
    /// </summary>
    public partial class BoxName : Window
    {
        public BoxName()
        {
            
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ownerWindow = (MainWindow)Owner;

            ownerWindow.dataTable.Columns.Add(this.Luc.Text);
            Owner.Show();
            this.Close();
            ownerWindow.dataGrid.ItemsSource = null;
            ownerWindow.dataGrid.ItemsSource = ownerWindow.dataTable.DefaultView;
        }
    }
}
