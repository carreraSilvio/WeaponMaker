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
    public partial class WeaponTypeEditPage : Page, INotifyPropertyChanged
    {
        private readonly SessionService _session;
        public WeaponType CurrentWeaponType
        {
            get => _session.CurrentWeaponType;
            set => _session.CurrentWeaponType = value;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public WeaponTypeEditPage()
        {
            _session = ServiceLocator.Fetch<SessionService>();
            InitializeComponent();
            WeaponTypeListBox.ItemsSource = _session.Project.WeaponTypes;
            WeaponTypeListBox.SelectedItem = WeaponTypeListBox.Items.GetItemAt(0);
        }

        private void WeaponListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Happens when you remove an item
            if (e.AddedItems.Count == 0)
            {
                _session.CurrentWeaponTypeIndex -= 1;
            }
            else
            {
                _session.CurrentWeaponTypeIndex = WeaponTypeListBox.Items.IndexOf(e.AddedItems[0]);
            }
            RaisePropertyChanged(nameof(CurrentWeaponType));
        }

        #region Bottom Buttons Handlers

        private void AddButton_Clicked(object sender, RoutedEventArgs e)
        {
            _session.Project.WeaponTypes.Add(new WeaponType() { Name = "New Weapon" });
            UpdateRemoveButtons();
        }

        private void RemoveButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (_session.Project.WeaponTypes.Count == 1) return;
            _session.Project.WeaponTypes.RemoveAt(_session.Project.WeaponTypes.Count - 1);

            UpdateRemoveButtons();
        }

        #endregion

        #region Context Menu Handlers

        private void CtxMenu_Cut_Clicked(object sender, EventArgs e)
        {
            try
            {
                var copiedWeaponType = new WeaponType();
                copiedWeaponType.Copy(_session.CurrentWeaponType);
                _session.CurrentWeaponType.Clear();

                Clipboard.SetData(typeof(WeaponType).FullName, copiedWeaponType);

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
                var copiedWeaponType = new WeaponType();
                copiedWeaponType.Copy(_session.CurrentWeaponType);

                Clipboard.SetData(typeof(WeaponType).FullName, copiedWeaponType);

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
                var copiedWeaponType = Clipboard.GetData(typeof(WeaponType).FullName) as WeaponType;

                //Set value
                _session.CurrentWeaponType.Copy(copiedWeaponType);

                //Update UI
                Clipboard.Clear();
                UpdateHasDataInClipboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CtxMenu_Duplicate_Clicked(object sender, RoutedEventArgs e)
        {
            var targetIndex = _session.CurrentWeaponTypeIndex;
            var copiedWeaponType = new WeaponType();
            copiedWeaponType.Copy(_session.CurrentWeaponType);

            _session.Project.WeaponTypes.Insert(targetIndex, copiedWeaponType);
            UpdateRemoveButtons();
        }

        private void CtxMenu_Delete_Clicked(object sender, RoutedEventArgs e)
        {
            if (_session.Project.WeaponTypes.Count == 1) return;
            _session.Project.WeaponTypes.RemoveAt(WeaponTypeListBox.SelectedIndex);

            UpdateRemoveButtons();
        }

        private void CtxMenu_MoveUp_Clicked(object sender, RoutedEventArgs e)
        {
            if (WeaponTypeListBox.SelectedIndex <= 0) return;

            var selectedIndex = WeaponTypeListBox.SelectedIndex;

            var itemToMoveUp = _session.Project.WeaponTypes[selectedIndex];
            _session.Project.WeaponTypes.RemoveAt(selectedIndex);
            _session.Project.WeaponTypes.Insert(selectedIndex - 1, itemToMoveUp);
            _session.CurrentWeaponTypeIndex = selectedIndex - 1;
        }

        private void CtxMenu_MoveDown_Clicked(object sender, RoutedEventArgs e)
        {
            if (WeaponTypeListBox.SelectedIndex >= WeaponTypeListBox.Items.Count - 1) return;

            var selectedIndex = WeaponTypeListBox.SelectedIndex;

            var itemToMoveDown = _session.Project.WeaponTypes[selectedIndex];
            _session.Project.WeaponTypes.RemoveAt(selectedIndex);
            _session.Project.WeaponTypes.Insert(selectedIndex + 1, itemToMoveDown);
            _session.CurrentWeaponTypeIndex = selectedIndex + 1;
        }
        #endregion

        private void UpdateRemoveButtons()
        {
            var hasOneItem = _session.Project.WeaponTypes.Count == 1;
            RemoveWeaponBtn.IsEnabled = !hasOneItem;
            CtxMenu_Delete.IsEnabled = !hasOneItem;
        }

        private void UpdateHasDataInClipboard()
        {
            //TODO: Not pretty. The codebehind should not change 
            //the UI directly but it's what we have for now.
            CtxMenu_Paste.IsEnabled = Clipboard.ContainsData(typeof(Weapon).FullName);
        }
    }
}
