using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DrillToLight.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrillToLight.ViewModels
{
    internal partial class MainViewModel : ObservableObject
    {
        // Chemin du nouveau fichier
        private string cheminNouveauFichier;
        public string CheminNomNouveauFichier
        {
            get => cheminNouveauFichier;
            set
            {
                if (SetProperty(ref cheminNouveauFichier, value))
                {
                    BtnEnregistrer.NotifyCanExecuteChanged();
                }
            }
        }

        // Chemin du fichier original
        private string cheminFichierOriginal;
        public string CheminFichierOriginal
        {
            get => cheminFichierOriginal;
            set => SetProperty(ref cheminFichierOriginal, value);
        }

        // Gcode original
        ObservableCollection<string> gcodeOriginal;
        public ObservableCollection<string> GcodeOriginal
        {
            get => gcodeOriginal;
            set => SetProperty(ref gcodeOriginal, value);
        }

        // Gcode modifié
        ObservableCollection<string> gcodeModif;
        public ObservableCollection<string> GcodeModif
        {
            get => gcodeModif;
            set => SetProperty(ref gcodeModif, value);
        }

        // Commande Bouton Parcourir
        private RelayCommand? btnParcourir;
        public RelayCommand? BtnParcourir => btnParcourir ?? (btnParcourir = new RelayCommand(() => { ExecuteParcourir(); }));

        // Affichage des gcodes dans les listBox
        public void ExecuteParcourir()
        {
            CheminFichierOriginal = _dialogue.Fichier();
            GcodeOriginal = _Lecture.GetGcode(CheminFichierOriginal);
            GcodeModif = _conversion.GetConvertir(GcodeOriginal);
            CheminNomNouveauFichier = CheminFichierOriginal.Insert(CheminFichierOriginal.Length - 3, "-Laser");
        }


        // Commande Bouton Enregistrer
        private RelayCommand btnEnregistrer;
        public RelayCommand BtnEnregistrer
        {
            get
            {
                return btnEnregistrer ?? (btnEnregistrer = new RelayCommand(ExecuteEnregistrer, () => !string.IsNullOrEmpty(CheminNomNouveauFichier)));
            }
        }

        // Commande d'enregistrement du gcode modifié
        public void ExecuteEnregistrer()
        {
            _enregistrement.Enregistrement(GcodeModif, CheminNomNouveauFichier);
            _dialogue.ShowMessage("Enregistrement", "Fichier modifié enregistré");
        }

        // Services
        IDialogue _dialogue;
        ILecture _Lecture;
        IConversion _conversion;
        IEnregistrement _enregistrement;

        public MainViewModel(IDialogue dialogue, ILecture lecture, IConversion conversion, IEnregistrement enregistrement)
        {
            _dialogue = dialogue;
            _Lecture = lecture;
            _conversion = conversion;
            _enregistrement = enregistrement;
        }
    }
}
