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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HandleNewProjectClicked(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditWindow();
            editWindow.Show();

            Close();
        }

        private void HandleOpenProjectClicked(object sender, RoutedEventArgs e)
        {
            var result = FileSystemService.OpenProject();
            if(result.success)
            {
                var editWindow = new EditWindow(result.project);
                editWindow.Show();
            }

            Close();
        }
    }
}
