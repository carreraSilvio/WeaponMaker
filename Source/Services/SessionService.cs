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
            //TODO Edit this later
            get => _project.FirstWeapon;
            set => _project.FirstWeapon = value;
        }

        public SessionService()
        {
            _project = new Project();
        }

    }
}
