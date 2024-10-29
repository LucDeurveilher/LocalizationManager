using System;
using System.Collections.Generic;
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
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("chemin_du_fichier.xml");
        }


    }
}
