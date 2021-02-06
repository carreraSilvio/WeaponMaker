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
            set => _project = value;
        }

        private Weapon _currentWeapon;
        public Weapon CurrentWeapon
        {
            get => _currentWeapon;
            set => _currentWeapon = value;
        }
        public SessionService()
        {
            _project = new Project();
            _currentWeapon = new Weapon();
        }

    }
}
