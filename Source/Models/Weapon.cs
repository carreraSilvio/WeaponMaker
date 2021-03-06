﻿using System;
using System.ComponentModel;
using System.Reflection;

namespace WeaponMaker
{
    [Serializable]
    public class Weapon : INotifyPropertyChanged
    {
        private string _name = "Weapon";
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
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
        private string _weaponTypeId = "";
        public string WeaponTypeId
        {
            get
            {
                return _weaponTypeId.ToString();
            }
            set
            {
                _weaponTypeId = value;
                RaisePropertyChanged(nameof(Damage));
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void Copy(Weapon otherWeapon)
        {
            PropertyInfo[] properties = typeof(Weapon).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                property.SetValue(this, property.GetValue(otherWeapon));
            }
        }

        public void Clear()
        {
            Name = "";
            Damage = "0";
            RateOfFire = "0";
        }
    }
}
