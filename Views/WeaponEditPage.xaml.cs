using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeaponMaker
{
    /// <summary>
    /// Interaction logic for WeaponEditPage.xaml
    /// </summary>
    public partial class WeaponEditPage : Page, INotifyPropertyChanged
    {
        private SessionService _session;
      
        public Weapon Weapon
        {
            get => _session.CurrentWeapon;
            set => _session.CurrentWeapon = value;
        }

        public WeaponEditPage()
        {
            _session = ServiceLocator.Fetch<SessionService>();
            InitializeComponent();
            WeaponListBox.ItemsSource = _session.Project.Weapons;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _session.CurrentWeaponIndex = WeaponListBox.Items.IndexOf(e.AddedItems[0]);
            RaisePropertyChanged(nameof(Weapon));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _session.Project.Weapons.Add(new Weapon() { Name = "New Weapon" });
        }
    }
}
