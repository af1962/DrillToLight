﻿using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Windows.Automation.Peers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DrillToLight.Services
{
    internal class Conversion : IConversion
    {
        ObservableCollection<string>? gcodeLaser;
        public ObservableCollection<string> GetConvertir(ObservableCollection<string> gcodeDrill)
        {
            gcodeLaser = new ObservableCollection<string>();
            string str = "";
            string[] tab;
            int index;

            // Recherche le dernier Z
            int fin = gcodeDrill.Count;
            while (!str.Contains('Z'))
            {
                str = gcodeDrill[fin - 1];
                fin--;
            }

            // Recherche le premier X
            str = "";
            int debut = 0;
            while (!str.Contains("G00"))
            {
                str = gcodeDrill[debut];
                debut++;
            }

            // Mise en du Gcode
            for (int i = debut-1; i < fin; i++)
            {
                // Remplace tous les Fxx par F150. Les F sur les lignes avec Z seront de toutes façons supprimés par la suite
                if (gcodeDrill[i].Contains('F'))
                {
                    tab = gcodeDrill[i].Split(' ');

                    // Recherche de l'index de l'élément commençant par un 'F'dans le tableau de string tab
                    index = Array.IndexOf(tab, Array.Find(tab, item => item.StartsWith("F", StringComparison.Ordinal)));
                    gcodeDrill[i] = gcodeDrill[i].Replace(tab[index], "F150");
                }

                // Change tous les Z en Z0. Avec activation et désactivation du laser
                if (gcodeDrill[i].Contains('Z'))
                {
                    if (gcodeDrill[i].Contains("Z0"))
                    {
                        gcodeLaser.Add("G0 Z0 M03 S100");
                    }
                    else
                    {
                        if (!gcodeDrill[i].Contains('-'))
                        {
                            gcodeLaser.Add("G0 Z0 M03 S0");
                        }
                    }
                }
                else
                {
                    gcodeLaser.Add(gcodeDrill[i]);
                }

            }

            // Arrêt du laser à la fin
            gcodeLaser.Add("M03 S0");

            return gcodeLaser;
        }
    }
}
