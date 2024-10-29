using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        private void ImportJson()
        {
            string json = File.ReadAllText(filePath);
        }

    }
}
