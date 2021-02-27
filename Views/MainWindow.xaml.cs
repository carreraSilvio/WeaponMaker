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
        private MessageCommand _messageCommand = new MessageCommand();
        public MessageCommand MessageCommand => _messageCommand;

        public MainWindow()
        {
            InitializeComponent();
            PreferencesService preferencesService = ServiceLocator.Fetch<PreferencesService>();
            if (!preferencesService.Preferences.LoadLastProjectOnStartUp)
            {
                return;
            }

            CommandService commandService = ServiceLocator.Fetch<CommandService>();
            if (commandService.Get<LoadProjectCommand>().Execute(preferencesService.Preferences.LastProjectPath))
            {
                var args = new NavigateToCommand.Args()
                {
                    caller = this,
                    target = typeof(EditWindow)
                };
                commandService.Get<NavigateToCommand>().Execute(args);
            }
        }

        private void NewProject_Clicked(object sender, RoutedEventArgs e)
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

        private void OpenProject_Clicked(object sender, RoutedEventArgs e)
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

        private void Preferences_Clicked(object sender, RoutedEventArgs e)
        {
            PreferencesDialog preferences = new PreferencesDialog();
            preferences.ShowDialog();
        }

        private void Exit_Clicked(object sender, RoutedEventArgs e)
        {
            var commandService = ServiceLocator.Fetch<CommandService>();
            commandService.Get<ShutdownCommand>().Execute();
        }
    }
}
