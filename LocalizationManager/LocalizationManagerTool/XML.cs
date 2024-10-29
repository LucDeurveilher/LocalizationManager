using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        private void ImportXML()
        {
            if (new FileInfo(filePath).Length == 0)
            {
                MessageBox.Show("Le fichier est vide.");
                return;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
        }


    }
}
