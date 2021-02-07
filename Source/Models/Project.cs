using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace WeaponMaker
{
    /// <summary>
    /// Represents a weapon maker project
    /// </summary>
    public class Project : INotifyPropertyChanged
    {
        private string _name;
        private List<Weapon> _weapons;
        private List<string> _weaponTypes;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public List<Weapon> Weapons { get => _weapons; set => _weapons = value; }

        public Project()
        {
            _weapons = new List<Weapon>
            {
                new Weapon()
                {
                    Name = "Pistol"
                },
                new Weapon()
                {
                    Name = "Shotgun"
                }
            };
            _name = "Project Name";
        }

        public void Copy(Project otherWeapon)
        {
            PropertyInfo[] properties = typeof(Project).GetProperties();
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
