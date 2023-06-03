using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrillToLight.Interfaces
{
    internal interface IModificationCode
    {
        ObservableCollection<string> GetModif(ObservableCollection<string> collection,string p,string s, string newP,string newS);
    }
}
