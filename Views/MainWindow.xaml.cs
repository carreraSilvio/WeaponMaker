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
        private NewProjectCommand _newProjectCommand = new NewProjectCommand();
        public NewProjectCommand NewProjectCommand  => _newProjectCommand;

        private MessageCommand _messageCommand = new MessageCommand();
        public MessageCommand MessageCommand => _messageCommand;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void HandleNewProjectClicked(object sender, RoutedEventArgs e)
        {
            var newProject = new Project();
            if (FileSystemService.SaveProject(newProject))
            {
                Close();
                var editWindow = new EditWindow();
                editWindow.Show();
            }
        }

        private void HandleOpenProjectClicked(object sender, RoutedEventArgs e)
        {
            var result = FileSystemService.OpenProject();
            if(result.success)
            {
                var session = ServiceLocator.Fetch<SessionService>();
                session.Project = result.project;
                var editWindow = new EditWindow();
                editWindow.Show();

                Close();
            }
        }
    }
}
