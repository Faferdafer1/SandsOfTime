using System;
using System.Collections.Generic;
using System.Linq;
using Greg.Weapons.Components;
using UnityEngine;

namespace Greg.Weapons
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data", order = 0)]
    public class WeaponDataSO : ScriptableObject
    {
        [field: SerializeField] public int NumberOfAttacks { get; private set; }

        [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

        public T GetData<T>()
        {
            return ComponentData.OfType<T>().FirstOrDefault();
        }

        public List<Type> GetAllDependencies()
        {
            return ComponentData.Select(component => component.ComponentDependency).ToList();
        }

        public void AddData(ComponentData data)
        {
            //if type of data already exists do not add the data again
            if (ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
                return;
            


            ComponentData.Add(data);
        }
    }
}
