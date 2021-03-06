﻿using System;
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

        public void WireEventHandlers()
        {
            Project.PropertyChanged -= Project_PropertyChanged;
            Project.PropertyChanged += Project_PropertyChanged;

            Project.Weapons.CollectionChanged -= Weapons_CollectionChanged;
            Project.Weapons.CollectionChanged += Weapons_CollectionChanged;

            Project.WeaponTypes.CollectionChanged -= WeaponTypes_CollectionChanged;
            Project.WeaponTypes.CollectionChanged += WeaponTypes_CollectionChanged;
        }

        private void Project_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Name" && e.PropertyName != "Path") return;

            IsProjectModified = true;
        }

        private void Weapons_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IsProjectModified = true;
        }

        private void WeaponTypes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IsProjectModified = true;
        }

    }
}
