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

        public Weapon CurrentWeapon
        {
            get => _session.CurrentWeapon;
            set => _session.CurrentWeapon = value;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        #endregion

        public WeaponEditPage()
        {
            _session = ServiceLocator.Fetch<SessionService>();
            InitializeComponent();

            //SetUp weaponType combobox
            WeaponTypesComboBox.ItemsSource = _session.Project.WeaponTypes;
            WeaponTypesComboBox.SelectedItem = WeaponTypesComboBox.Items.GetItemAt(0);

            //SetUp weapons listbox
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

                UpdateWeaponTypeComboBoxSelection();
            }
            RaisePropertyChanged(nameof(CurrentWeapon));


        }

        #region Bottom Buttons Handlers

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

        #endregion

        #region Context Menu Handlers

        private void CtxMenu_Cut_Clicked(object sender, EventArgs e)
        {
            try
            {
                Weapon copiedWeapon = new Weapon();
                copiedWeapon.Copy(_session.CurrentWeapon);
                _session.CurrentWeapon.Clear();

                Clipboard.SetData(typeof(Weapon).FullName, copiedWeapon);

                //Update UI
                UpdateHasDataInClipboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CtxMenu_Copy_Clicked(object sender, EventArgs e)
        {
            try
            {
                Weapon copiedWeapon = new Weapon();
                copiedWeapon.Copy(_session.CurrentWeapon);

                Clipboard.SetData(typeof(Weapon).FullName, copiedWeapon);

                //Update UI
                UpdateHasDataInClipboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CtxMenu_Paste_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Fetch value from clipboard
                var copiedWeapon = Clipboard.GetData(typeof(Weapon).FullName) as Weapon;

                //Set value
                _session.CurrentWeapon.Copy(copiedWeapon);
                

                //Update UI
                Clipboard.Clear();
                UpdateHasDataInClipboard();
                UpdateWeaponTypeComboBoxSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CtxMenu_Duplicate_Clicked(object sender, RoutedEventArgs e)
        {
            var targetIndex = _session.CurrentWeaponIndex;
            Weapon copiedWeapon = new Weapon();
            copiedWeapon.Copy(_session.CurrentWeapon);

            _session.Project.Weapons.Insert(targetIndex, copiedWeapon);
            UpdateRemoveButtons();
        }
        
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

        #region Drag-And-Drop


        private void WeaponListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        void WeaponListBox_Drop(object sender, DragEventArgs e)
        {


            //ListBox parent = (ListBox)sender;
            //Weapon data = (Weapon)e.Data.GetData(typeof(Weapon));
            //var tst = parent.DataContext as Weapon;
            //_session.Project.Weapons.Remove(data);
            //_session.Project.Weapons.Add(data);
            //_session.CurrentWeaponIndex = _session.Project.Weapons.IndexOf(data);

            //var parent = (ListBox)sender;
            //var droppedData = (Weapon)e.Data.GetData(typeof(Weapon));

            //int removedIdx = WeaponListBox.Items.IndexOf(droppedData);
            //int targetIdx = WeaponListBox.Items.IndexOf(WeaponListBox.SelectedItem);

            //var droppedData = e.Data.GetData(typeof(Weapon)) as Weapon;
            //var target = ((ListBoxItem)(sender)).DataContext as Weapon;

            //int removedIdx = WeaponListBox.Items.IndexOf(droppedData);
            //int targetIdx = WeaponListBox.Items.IndexOf(target);

            //if (removedIdx < targetIdx)
            //{
            //    _session.Project.Weapons.Insert(targetIdx + 1, droppedData);
            //    _session.Project.Weapons.RemoveAt(removedIdx);
            //}
            //else
            //{
            //    int remIdx = removedIdx + 1;
            //    if (_session.Project.Weapons.Count + 1 > remIdx)
            //    {
            //        _session.Project.Weapons.Insert(targetIdx, droppedData);
            //        _session.Project.Weapons.RemoveAt(remIdx);
            //    }
            //}
            //WeaponListBox.SelectedItem = droppedData;
        }

        #endregion


        private void WeaponTypesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                var selectedWeaponType = WeaponTypesComboBox.SelectedItem as WeaponType;
                CurrentWeapon.WeaponTypeId = selectedWeaponType.Id;
            }
        }

        private void UpdateRemoveButtons()
        {
            var hasOneItem = _session.Project.Weapons.Count == 1;
            RemoveWeaponBtn.IsEnabled = !hasOneItem;
            CtxMenu_Delete.IsEnabled = !hasOneItem;
        }

        private void UpdateHasDataInClipboard()
        {
            //TODO: Not pretty. The codebehind should not change 
            //the UI directly but it's what we have for now.
            CtxMenu_Paste.IsEnabled = Clipboard.ContainsData(typeof(Weapon).FullName);
        }

        private void UpdateWeaponTypeComboBoxSelection()
        {
            var currentWeaponTypeId = _session.CurrentWeapon.WeaponTypeId;
            if (currentWeaponTypeId != "")
            {
                WeaponTypesComboBox.SelectedItem = _session.Project.FetchWeaponType(currentWeaponTypeId);
            }
            else
            {
                WeaponTypesComboBox.SelectedItem = WeaponTypesComboBox.Items[0];
            }
        }

    }
}
