using CommunityToolkit.Mvvm.Input;
using DrillToLight.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DrillToLight.ViewModels
{
    internal partial class MainViewModel : ObservableObject
    {
        // Vitesse courante
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ModificationCodeCommand))]
        private string? speedCurrent;

        // Vitesse souhaitéé
        [ObservableProperty]
        private string speedNew;

        // Puissance courante
        [ObservableProperty]
        private string? powerCurrent;

        // Puissance souhaitée
        [ObservableProperty]
        private string powerNew;

        // Chemin du nouveau fichier
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EnregistrementCommand))]
        private string cheminNomNouveauFichier;

        // Chemin du fichier original
        [ObservableProperty]
        private string? cheminFichierOriginal;

        // Gcode original
        [ObservableProperty]
        ObservableCollection<string> gcodeOriginal;

        // Gcode modifié
        [ObservableProperty]
        ObservableCollection<string> gcodeModif;

        [ObservableProperty]
        private bool infoChargement;

        // Commande Bouton Parcourir
        [RelayCommand]
        private void Parcourir()
        {
            CheminFichierOriginal = _dialogue.Fichier();
         
            if (!string.IsNullOrEmpty(CheminFichierOriginal))
            {               
                Task tache = ExecuteParcourir();
                InfoChargement = true;
            }
        }

        // Affichage des gcodes dans les listBox
        public async Task ExecuteParcourir()
        {
            GcodeOriginal.Clear(); GcodeModif.Clear();

            await Task.Run(() =>
            {
                GcodeOriginal = _Lecture.GetGcode(CheminFichierOriginal);
                GcodeModif = _conversion.GetConvertir(GcodeOriginal);
                InfoChargement = false;
            });
            CheminNomNouveauFichier = CheminFichierOriginal.Insert(CheminFichierOriginal.Length - 3, "-Laser");
            Analyse();
        }

        // Bouton modification
        [RelayCommand(CanExecute = nameof(CanExecuteModificationCode))]
        private void ModificationCode()
        {
            GcodeModif = _modificationCode.GetModif(GcodeModif, PowerCurrent, SpeedCurrent, PowerNew, SpeedNew);
            PowerCurrent = "S" + PowerNew;
            SpeedCurrent = "F" + SpeedNew;
            SpeedNew = "";
            PowerNew = "";
        }

        // CanExecute ModificationCode
        private bool CanExecuteModificationCode()
        {
            return !string.IsNullOrEmpty(SpeedCurrent);
        }

        // Commande Bouton Enregistrer
        [RelayCommand(CanExecute = nameof(CanExecuteEnregistrement))]
        private void Enregistrement()
        {
            _enregistrement.Enregistrement(GcodeModif, CheminNomNouveauFichier);
            _dialogue.ShowMessage("Enregistrement", "Fichier modifié enregistré");
        }

        // CanExecute Enregistrement fichier
        private bool CanExecuteEnregistrement()
        {
            return !string.IsNullOrEmpty(CheminNomNouveauFichier);
        }

        // Services
        IDialogue _dialogue;
        ILecture _Lecture;
        IConversion _conversion;
        IEnregistrement _enregistrement;
        IModificationCode _modificationCode;

        public MainViewModel(IDialogue dialogue, ILecture lecture, IConversion conversion, IEnregistrement enregistrement, IModificationCode modificationCode)
        {
            _dialogue = dialogue;
            _Lecture = lecture;
            _conversion = conversion;
            _enregistrement = enregistrement;
            GcodeOriginal = new ObservableCollection<string>();
            gcodeModif = new ObservableCollection<string>();
            _modificationCode = modificationCode;
           
        }

        /// <summary>
        /// Détermine la puissance et la vitesse par défaut
        /// </summary>
        public void Analyse()
        {
            string[] tab;
            tab = GcodeModif[1].Split(' ');
            PowerCurrent = tab[1];
            tab = GcodeModif[2].Split(' ');
            SpeedCurrent = tab[1];
        }
    }
}
