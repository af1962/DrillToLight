using DrillToLight.Services;
using DrillToLight.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DrillToLight
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();

            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();                 // necessite "using Microsoft.Extensions.DependencyInjection"

            // Services
            services.AddSingleton<IDialogue, Dialogue>();
            services.AddSingleton<ILecture, Lecture>();
            services.AddSingleton<IConversion, Conversion>();
            services.AddSingleton<IEnregistrement, EnregistrementFichier>();
            services.AddScoped<IModificationCode, ModificationCode>();


            // Viewmodels
            services.AddTransient<MainViewModel>();


            return services.BuildServiceProvider();
        }

    }
}
