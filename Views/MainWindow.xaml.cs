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
using MessageBox = System.Windows.Forms.MessageBox;

namespace WeaponMaker
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public string StatusBarMessage
        {
            get => _statusBarMessage;
            set
            {
                _statusBarMessage = value;
                RaisePropertyChanged(nameof(StatusBarMessage));
            }
        }

        private string _statusBarMessage;

        private readonly WeaponEditPage _weaponEditPage;
        private readonly ProjectEditPage _projectEditPage;
        private readonly WeaponTypeEditPage _weaponTypeEditPage;

        private readonly SessionService _session;
        private readonly CommandService _commandService;

        public MainWindow()
        {
            _session = ServiceLocator.Fetch<SessionService>();
            InitializeComponent();

            _weaponEditPage = new WeaponEditPage();
            _projectEditPage = new ProjectEditPage();
            _weaponTypeEditPage = new WeaponTypeEditPage();
            _mainFrame.Navigate(_weaponEditPage);

            _commandService = ServiceLocator.Fetch<CommandService>();
            StatusBarMessage = $"Loaded {_session.Project.Name} project";
            FadeOutStatusBarMessage(10);
            SetUpHotkeys();
        }

        #region New/Open/Save
        private void HandleNewProjectClicked(object sender, RoutedEventArgs e)
        {
            if (_commandService.Get<NewProjectCommand>().Execute())
            {
                var args = new NavigateToCommand.Args()
                {
                    caller = this,
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
                    caller = this,
                    target = typeof(MainWindow)
                };
                _commandService.Get<NavigateToCommand>().Execute(args);
            }
        }

        private void HandleSaveProjectClicked(object sender, RoutedEventArgs e)
        {
            ExecuteSaveProject();
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

        #region Dialogs
        private void HandlePreferencesClicked(object sender, RoutedEventArgs e)
        {
            PreferencesDialog preferences = new PreferencesDialog();
            preferences.ShowDialog();
        } 
        #endregion

        #region Views
        private void HandleWeaponsEditViewClicked(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(_weaponEditPage);
        }

        private void HandleWeaponTypesEditViewClicked(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(_weaponTypeEditPage);
        }

        private void HandleProjectEditViewClicked(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(_projectEditPage);
        }


        #endregion

        #region Exit
        private void Exit_Clicked(object sender, RoutedEventArgs e)
        {
            if(_commandService.Get<CheckChangesCommand>().Execute())
            {
                _commandService.Get<ShutdownCommand>().Execute();
            }
        } 
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void SetUpHotkeys()
        {
            try
            {
                RoutedCommand firstSettings = new RoutedCommand();
                firstSettings.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(firstSettings, HandleCtrlSHotkey));
            }
            catch (Exception err)
            {
                //handle exception error
            }
        }
        private void HandleCtrlSHotkey(object sender, ExecutedRoutedEventArgs e)
        {
            ExecuteSaveProject();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _commandService.Get<CheckChangesCommand>().Execute();
        }


        private void ExecuteSaveProject()
        {
            var success = _commandService.Get<SaveProjectCommand>().Execute();
            if (success)
            {
                StatusBarMessage = $"Saved {_session.Project.LastTimeSaved}";
                HideStatusBarMessage(10);
            }
        }

        /// <summary>
        /// Will fade out the status bar over X seconds
        /// </summary>
        private void FadeOutStatusBarMessage(double fadeDuration = 3)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(fadeDuration),
                EasingFunction = new QuadraticEase()
            };

            Storyboard sb = new Storyboard();
            sb.Children.Add(animation);

            StatusBarTextBlock.Opacity = 1;
            StatusBarTextBlock.Visibility = Visibility.Visible;

            Storyboard.SetTarget(sb, StatusBarTextBlock);
            Storyboard.SetTargetProperty(sb, new PropertyPath(System.Windows.Controls.Control.OpacityProperty));

            sb.Completed += (object sender, EventArgs e) => 
            {
                StatusBarTextBlock.Visibility = Visibility.Collapsed;
            } ;
            sb.Begin();
        }

        /// <summary>
        /// Will hide the status bar after x seconds
        /// </summary>
        private void HideStatusBarMessage(double delay = 3)
        {
            var animation = new DoubleAnimation
            {
                To = 1,
                From = 1,
                Duration = TimeSpan.FromSeconds(delay),
                EasingFunction = new QuadraticEase()
            };

            Storyboard sb = new Storyboard();
            sb.Children.Add(animation);

            StatusBarTextBlock.Opacity = 1;
            StatusBarTextBlock.Visibility = Visibility.Visible;

            Storyboard.SetTarget(sb, StatusBarTextBlock);
            Storyboard.SetTargetProperty(sb, new PropertyPath(System.Windows.Controls.Control.OpacityProperty));

            sb.Completed += (object sender, EventArgs e) =>
            {
                StatusBarTextBlock.Visibility = Visibility.Collapsed;
            };
            sb.Begin();
        }
    }

}
