using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrillToLight.Interfaces
{
    internal interface IEnregistrement
    {
        void Enregistrement(ObservableCollection<string> collection,string chemin);
    }
}
