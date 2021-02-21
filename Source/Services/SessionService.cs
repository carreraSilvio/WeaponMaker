using System;
using System.Collections.Generic;
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

        public SessionService()
        {
            _project = new Project();
        }

        public void Clear()
        {
            CurrentWeaponIndex = 0;
        }

    }
}
