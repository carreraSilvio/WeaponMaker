using System;
using System.ComponentModel;
using System.Reflection;

namespace WeaponMaker
{
    public class Weapon : INotifyPropertyChanged
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

        public void Copy(Weapon otherWeapon)
        {
            PropertyInfo[] properties = typeof(Weapon).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                property.SetValue(this, property.GetValue(otherWeapon));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
