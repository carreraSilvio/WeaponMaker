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
            WeaponListBox.SelectedItem = WeaponListBox.Items.GetItemAt(0);
        }

        private void WeaponListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Happens when you remove an item
            if (e.AddedItems.Count == 0)
            {
                _session.CurrentWeaponIndex -= 1;
            }
            else
            {
                _session.CurrentWeaponIndex = WeaponListBox.Items.IndexOf(e.AddedItems[0]);
            }
            RaisePropertyChanged(nameof(Weapon));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddButton_Clicked(object sender, RoutedEventArgs e)
        {
            _session.Project.Weapons.Add(new Weapon() { Name = "New Weapon" });
            UpdateRemoveButtons();
        }

        private void RemoveButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (_session.Project.Weapons.Count == 1) return;
            _session.Project.Weapons.RemoveAt(_session.Project.Weapons.Count - 1);

            UpdateRemoveButtons();
        }

        #region Context Menu Handlers

        private void CtxMenu_Delete_Clicked(object sender, RoutedEventArgs e)
        {
            if (_session.Project.Weapons.Count == 1) return;
            _session.Project.Weapons.RemoveAt(WeaponListBox.SelectedIndex);

            UpdateRemoveButtons();
        }

        private void CtxMenu_MoveUp_Clicked(object sender, RoutedEventArgs e)
        {
            if (WeaponListBox.SelectedIndex <= 0) return;

            var selectedIndex = WeaponListBox.SelectedIndex;

            var itemToMoveUp = _session.Project.Weapons[selectedIndex];
            _session.Project.Weapons.RemoveAt(selectedIndex);
            _session.Project.Weapons.Insert(selectedIndex - 1, itemToMoveUp);
            _session.CurrentWeaponIndex = selectedIndex - 1;
        }

        private void CtxMenu_MoveDown_Clicked(object sender, RoutedEventArgs e)
        {
            if (WeaponListBox.SelectedIndex >= WeaponListBox.Items.Count - 1) return;

            var selectedIndex = WeaponListBox.SelectedIndex;

            var itemToMoveDown = _session.Project.Weapons[selectedIndex];
            _session.Project.Weapons.RemoveAt(selectedIndex);
            _session.Project.Weapons.Insert(selectedIndex + 1, itemToMoveDown);
            _session.CurrentWeaponIndex = selectedIndex + 1;
        } 
        #endregion

        private void UpdateRemoveButtons()
        {
            var hasOneItem = _session.Project.Weapons.Count == 1;
            RemoveWeaponBtn.IsEnabled = !hasOneItem;
            CtxMenuRemoveWeapon.IsEnabled = !hasOneItem;
        }
    }
}
