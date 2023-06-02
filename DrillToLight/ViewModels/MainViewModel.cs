using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DrillToLight.Interfaces;
using System.Collections.ObjectModel;

namespace DrillToLight.ViewModels
{
    internal partial class MainViewModel : ObservableObject
    {
        // Chemin du nouveau fichier
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(BtnEnregistrer))]
        private string cheminNomNouveauFichier;

        // Chemin du fichier original
        [ObservableProperty]
        private string cheminFichierOriginal;

        // Gcode original
        [ObservableProperty]
        ObservableCollection<string> gcodeOriginal;

        // Gcode modifié
        [ObservableProperty]
        ObservableCollection<string> gcodeModif;

        // Commande Bouton Parcourir
        private RelayCommand btnParcourir;
        public RelayCommand BtnParcourir => btnParcourir ??= new RelayCommand(() => { ExecuteParcourir(); });

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
        public RelayCommand BtnEnregistrer => btnEnregistrer ??= new RelayCommand(ExecuteEnregistrer, () => !string.IsNullOrEmpty(CheminNomNouveauFichier));

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
