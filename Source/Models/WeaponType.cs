using System;
using System.ComponentModel;
using System.Reflection;

namespace WeaponMaker
{
    [Serializable]
    public class WeaponType : INotifyPropertyChanged
    {
        private string _name = "Handgun";
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        private readonly string _id;
        public string Id
        {
            get
            {
                return _id;
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public WeaponType()
        {
            _id = Guid.NewGuid().ToString();
        }

        public void Copy(WeaponType otherWeapon)
        {
            PropertyInfo[] properties = typeof(Weapon).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name.Equals(nameof(Id))) continue;
                property.SetValue(this, property.GetValue(otherWeapon));
            }
        }

        public void Clear()
        {
            Name = "Handgun";
        }
    }
}
