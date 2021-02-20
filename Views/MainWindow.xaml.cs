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

        private NewProjectCommand _newProjectCommand = new NewProjectCommand();
        private OpenProjectCommand _openProjectCommand = new OpenProjectCommand();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void HandleNewProjectClicked(object sender, RoutedEventArgs e)
        {
            var args = new NewProjectCommand.Args()
            {
                caller = this,
                target = typeof(EditWindow)
            };
            _newProjectCommand.Execute(args);
        }

        private void HandleOpenProjectClicked(object sender, RoutedEventArgs e)
        {
            var args = new OpenProjectCommand.Args()
            {
                caller = this,
                target = typeof(EditWindow)
            };
            _openProjectCommand.Execute(args);
        }
    }
}
