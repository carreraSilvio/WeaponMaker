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

        private string _id;
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        [NonSerialized]
        private static readonly string[] IGNORED_COPY_PROPERTIES = new string[] {nameof(Id) };

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public WeaponType()
        {
            _id = Guid.NewGuid().ToString().Substring(0, 8);
        }

        public void Copy(WeaponType otherWeapon) 
        {
            Copier.CopyProperties<WeaponType>(this, otherWeapon, IGNORED_COPY_PROPERTIES);
        }

        public void Clear()
        {
            Name = "Handgun";
        }
    }
}
