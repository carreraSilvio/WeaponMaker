using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Weapon> _weapons;
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
        public ObservableCollection<Weapon> Weapons { get => _weapons; set => _weapons = value; }

        private string _path;
        public string Path { get => _path; set => _path = value; }

        public Project()
        {
           
        }

        public void Copy(Project otherProject)
        {
            PropertyInfo[] properties = typeof(Project).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                property.SetValue(this, property.GetValue(otherProject));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
