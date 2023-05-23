using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Greg.Weapons.Components;
using UnityEngine;

namespace Greg.Weapons
{
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private WeaponDataSO data;

        private List<WeaponComponent> componentsAlreadyOnWeapon = new List<WeaponComponent>();

        private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();

        private List<Type> componentDependencies = new List<Type>();

        private void Start()
        {
            GenerateWeapon(data);
        }

        [ContextMenu("Test Generate")]
        private void TestGeneration()
        {
            GenerateWeapon(data);
        }

        public void GenerateWeapon(WeaponDataSO data)
        {
            weapon.SetData(data);

            componentsAlreadyOnWeapon.Clear();
            componentsAddedToWeapon.Clear();
            componentDependencies.Clear();

            componentsAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();

            componentDependencies = data.GetAllDependencies();

            foreach (var dependency in componentDependencies)
            {
                //checks to see if duplicate if it is duplicate do not continue
                if (componentsAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency))
                    continue;

                //checks to see if it has weapon component or not
                var weaponComponent = componentsAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

                //adds weapon component
                if(weaponComponent == null)
                {
                    weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
                }

                weaponComponent.Init();

                componentsAddedToWeapon.Add(weaponComponent);
            }

            //looks at every item and removes any added to weapon which were not added by dependencys
            var componentsToRemove = componentsAlreadyOnWeapon.Except(componentsAddedToWeapon);

            foreach( var weaponComponent in componentsToRemove)
            {
                Destroy(weaponComponent);
            }
        }
    }
}
