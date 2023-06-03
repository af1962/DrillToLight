using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DrillToLight.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DrillToLight.ViewModels
{
    internal partial class MainViewModel : ObservableObject
    {
        // Vitesse courante
        [ObservableProperty]
        private string speedCurrent;

        // Vitesse souhaitéé
        [ObservableProperty]
        private string speedNew;

        // Puissance courante
        [ObservableProperty]
        private string powerCurrent;

        // Puissance souhaitée
        [ObservableProperty]
        private string powerNew;

        // Chemin du nouveau fichier
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(BtnEnregistrer))]
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
        private string? infoChargement = null;

        // Commande Bouton Parcourir
        private AsyncRelayCommand? btnParcourir;
        public AsyncRelayCommand? BtnParcourir
        {
            get
            {
                return btnParcourir ?? (new AsyncRelayCommand(ExecuteParcourir));
            }
        }

        // Affichage des gcodes dans les listBox
        public async Task ExecuteParcourir()
        {
            GcodeOriginal.Clear(); GcodeModif.Clear();
            CheminFichierOriginal = _dialogue.Fichier();
            InfoChargement = CheminFichierOriginal;
            await Task.Run(() =>
            {
                GcodeOriginal = _Lecture.GetGcode(CheminFichierOriginal);
                GcodeModif = _conversion.GetConvertir(GcodeOriginal);
                InfoChargement = null;
            });
            CheminNomNouveauFichier = CheminFichierOriginal.Insert(CheminFichierOriginal.Length - 3, "-Laser");
            Analyse();
        }

        private RelayCommand btnModificationCode;
        public RelayCommand BtnModificationCode => btnModificationCode ?? new RelayCommand(() => { ExecuteModificationCode(); });
    
        public void ExecuteModificationCode()
        {
            GcodeModif = _modificationCode.GetModif(GcodeModif, PowerCurrent, SpeedCurrent, PowerNew, SpeedNew);
            PowerCurrent="S"+PowerNew;
            SpeedCurrent="F"+SpeedNew;
            SpeedNew = "";
            PowerNew = "";
        }

        // Commande Bouton Enregistrer
        private RelayCommand? btnEnregistrer;
        public RelayCommand? BtnEnregistrer => btnEnregistrer ??= new RelayCommand(ExecuteEnregistrer, () => !string.IsNullOrEmpty(CheminNomNouveauFichier));

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
