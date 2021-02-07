using System;
using System.Collections.Generic;
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
    public partial class WeaponEditPage : Page
    {
        private SessionService _session;
   
        public Weapon Weapon
        {
            get => _session.CurrentWeapon;
            set => _session.CurrentWeapon.Copy(value);
        }

        public WeaponEditPage()
        {
            _session = ServiceLocator.Fetch<SessionService>();
            InitializeComponent();
        }
    }
}
