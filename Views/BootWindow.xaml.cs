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
    public partial class BootWindow : Window
    {
        private readonly CommandService _commandService;
        private readonly PreferencesService _preferencesService;

        public BootWindow()
        {
            InitializeComponent();
            _commandService = ServiceLocator.Fetch<CommandService>();
            _preferencesService = ServiceLocator.Fetch<PreferencesService>();
            if (!_preferencesService.Preferences.LoadLastProjectOnStartUp)
            {
                return;
            }


            if (_commandService.Get<LoadProjectCommand>().Execute(_preferencesService.Preferences.LastProjectPath))
            {
                var args = new NavigateToCommand.Args()
                {
                    currentWindow = this,
                    target = typeof(MainWindow)
                };
                _commandService.Get<NavigateToCommand>().Execute(args);
            }
        }

        private void HandleNewProjectClicked(object sender, RoutedEventArgs e)
        {
            if (_commandService.Get<NewProjectCommand>().Execute())
            {
                var args = new NavigateToCommand.Args()
                {
                    currentWindow = this,
                    target = typeof(MainWindow)
                };
                _commandService.Get<NavigateToCommand>().Execute(args);
            }
        }

        private void HandleOpenProjectClicked(object sender, RoutedEventArgs e)
        {
            if (_commandService.Get<OpenProjectCommand>().Execute())
            {
                var args = new NavigateToCommand.Args()
                {
                    currentWindow = this,
                    target = typeof(MainWindow)
                };
                _commandService.Get<NavigateToCommand>().Execute(args);
            }
        }

        private void HandlePreferencesClicked(object sender, RoutedEventArgs e)
        {
            PreferencesDialog prefsDialog = new PreferencesDialog();
            prefsDialog.ShowDialog();
        }

        private void HandleExitClicked(object sender, RoutedEventArgs e)
        {
            _commandService.Get<ShutdownCommand>().Execute();
        }
    }
}
