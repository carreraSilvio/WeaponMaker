using System.Windows;

namespace WeaponMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Weapon _weapon;
        public Weapon Weapon
        {
            get => _weapon;
            set => _weapon = value;
        }

        public MainWindow()
        {
            _weapon = new Weapon();
            InitializeComponent();
        }

        private void HandleExportJsonClicked(object sender, RoutedEventArgs e)
        {

        }

    }
}
