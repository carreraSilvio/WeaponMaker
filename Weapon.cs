using System;
using System.ComponentModel;

namespace WeaponMaker
{
    public class Weapon
    {
        private string _weaponName = "Pistol";

        public string WeaponName
        {
            get
            {
                return _weaponName;
            }
            set
            {
                _weaponName = value;
                RaisePropertyChanged(nameof(WeaponName));
            }
        }

        private float _rateOfFire;

        public string RateOfFire
        {
            get
            {
                return _rateOfFire.ToString();
            }
            set
            {
                var isNumeric = float.TryParse(value, out float n);
                _rateOfFire = isNumeric ? n : 0;
                RaisePropertyChanged(nameof(RateOfFire));
            }
        }

        private int _damage;

        public string Damage
        {
            get
            {
                return _damage.ToString();
            }
            set
            {
                var isNumeric = int.TryParse(value, out int n);
                _damage = isNumeric ? n : 0;
                RaisePropertyChanged(nameof(Damage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
