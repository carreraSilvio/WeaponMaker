using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Navigation;
using Newtonsoft.Json;

namespace WeaponMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CommandService _commandService;
        private readonly PreferencesService _preferencesService;

        public MainWindow()
        {
            InitializeComponent();
            _preferencesService = ServiceLocator.Fetch<PreferencesService>();
            if (!_preferencesService.Preferences.LoadLastProjectOnStartUp)
            {
                return;
            }

            _commandService = ServiceLocator.Fetch<CommandService>();
            if (_commandService.Get<LoadProjectCommand>().Execute(_preferencesService.Preferences.LastProjectPath))
            {
                var args = new NavigateToCommand.Args()
                {
                    caller = this,
                    target = typeof(EditWindow)
                };
                _commandService.Get<NavigateToCommand>().Execute(args);
            }
        }

        private void HandleNewProjectClicked(object sender, RoutedEventArgs e)
        {
            CommandService commandService = ServiceLocator.Fetch<CommandService>();
            if (commandService.Get<NewProjectCommand>().Execute())
            {
                var args = new NavigateToCommand.Args()
                {
                    caller = this,
                    target = typeof(EditWindow)
                };
                commandService.Get<NavigateToCommand>().Execute(args);
            }
        }

        private void HandleOpenProjectClicked(object sender, RoutedEventArgs e)
        {
            CommandService commandService = ServiceLocator.Fetch<CommandService>();
            if (commandService.Get<OpenProjectCommand>().Execute())
            {
                var args = new NavigateToCommand.Args()
                {
                    caller = this,
                    target = typeof(EditWindow)
                };
                commandService.Get<NavigateToCommand>().Execute(args);
            }
        }

        private void HandlePreferencesClicked(object sender, RoutedEventArgs e)
        {
            PreferencesDialog preferences = new PreferencesDialog();
            preferences.ShowDialog();
        }

        private void HandleExitClicked(object sender, RoutedEventArgs e)
        {
            
            commandService.Get<ShutdownCommand>().Execute();
        }
    }
}
