using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WeaponMaker
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window, INotifyPropertyChanged
    {
        public string StatusBarMessage
        {
            get => _statusBarMessage;
            set
            {
                _statusBarMessage = value;
                HideStatusBarMessage();
                RaisePropertyChanged(nameof(StatusBarMessage));
            }
        }

        private string _statusBarMessage;

        private readonly WeaponEditPage _weaponEditPage;
        private readonly ProjectEditPage _projectEditPage;

        private readonly SessionService _session;
        private readonly CommandService _commandService;

        public EditWindow()
        {
            _session = ServiceLocator.Fetch<SessionService>();
            InitializeComponent();

            _weaponEditPage = new WeaponEditPage();
            _projectEditPage = new ProjectEditPage();
            _mainFrame.Navigate(_weaponEditPage);

            _commandService = ServiceLocator.Fetch<CommandService>();
            StatusBarMessage = $"Loaded {_session.Project.Name} project";
        }

        #region New/Open/Save
        private void HandleNewProjectClicked(object sender, RoutedEventArgs e)
        {
            if (_commandService.Get<NewProjectCommand>().Execute())
            {
                var args = new NavigateToCommand.Args()
                {
                    caller = this,
                    target = typeof(EditWindow)
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
                    caller = this,
                    target = typeof(EditWindow)
                };
                _commandService.Get<NavigateToCommand>().Execute(args);
            }
        }

        private void HandleSaveProjectClicked(object sender, RoutedEventArgs e)
        {
            var success = _commandService.Get<SaveProjectCommand>().Execute();
            if (success)
            {
                StatusBarMessage = $"Saved {_session.Project.LastTimeSaved}";
            }
        }

        private void HandleSaveProjectAsClicked(object sender, RoutedEventArgs e)
        {
            _commandService.Get<SaveProjectAsCommand>().Execute();
        }
        private void HandleExportClicked(object sender, RoutedEventArgs e)
        {
            _commandService.Get<ExportProjectCommand>().Execute(_session.Project);
        }
        #endregion

        private void HandlePreferencesClicked(object sender, RoutedEventArgs e)
        {
            PreferencesDialog preferences = new PreferencesDialog();
            preferences.ShowDialog();
        }

        #region View Clicked
        private void HandleWeaponsEditViewClicked(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(_weaponEditPage);
        }

        private void HandleProjectEditViewClicked(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(_projectEditPage);
        }
        #endregion

        private void Exit_Clicked(object sender, RoutedEventArgs e)
        {
            var commandService = ServiceLocator.Fetch<CommandService>();
            commandService.Get<ShutdownCommand>().Execute();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void HideStatusBarMessage()
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                To = 0,
                From = 1,
                Duration = TimeSpan.FromSeconds(30),
                EasingFunction = new QuadraticEase()
            };

            Storyboard sb = new Storyboard();
            sb.Children.Add(animation);

            StatusBarTextBlock.Opacity = 1;
            StatusBarTextBlock.Visibility = Visibility.Visible;

            Storyboard.SetTarget(sb, StatusBarTextBlock);
            Storyboard.SetTargetProperty(sb, new PropertyPath(System.Windows.Controls.Control.OpacityProperty));

            sb.Completed += HandleStatusBarMessageFadeComplete;
            sb.Begin();
        }

        private void HandleStatusBarMessageFadeComplete(object sender, EventArgs e)
        {
            StatusBarTextBlock.Visibility = Visibility.Collapsed;
        }
    }

}
