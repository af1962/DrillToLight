﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrillToLight.Interfaces
{
    internal interface IConversion
    {
        ObservableCollection<string> GetConvertir(ObservableCollection<string> collection);
    }
}
