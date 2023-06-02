using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrillToLight.Interfaces
{
    internal interface IDialogue
    {
        bool YesNo(string title, string message);
        void ShowMessage(string title, string message);
        void ShowError(string title, string message);
        void ShowStop(string title, string message);       
        string Fichier();
    }
}
