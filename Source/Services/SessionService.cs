using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponMaker
{
    public class SessionService : IService
    {
        private Project _project;
        public Project Project
        {
            get => _project;
            set => _project.Copy(value);
        }

        public Weapon CurrentWeapon
        {
            get => _project.Weapons[CurrentWeaponIndex];
            set => _project.Weapons[CurrentWeaponIndex].Copy(value);
        }
        public int CurrentWeaponIndex { get; set; } = 0;

        public WeaponType CurrentWeaponType
        {
            get => _project.WeaponTypes[CurrentWeaponTypeIndex];
            set => _project.WeaponTypes[CurrentWeaponTypeIndex].Copy(value);
        }
        public int CurrentWeaponTypeIndex { get; set; } = 0;


        public bool IsProjectModified { get; set; }


        public SessionService()
        {
            _project = new Project();
        }

        public void Clear()
        {
            CurrentWeaponIndex = 0;
        }

        internal void Project_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Name" && e.PropertyName != "Path") return;

            IsProjectModified = true;
        }

    }
}
