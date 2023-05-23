using Cinemachine;
using Greg.Weapons.Components;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Callbacks;
using System.Linq;

namespace Greg.Weapons
{
    [CustomEditor(typeof(WeaponDataSO))]
    public class WeaponDataSOEditor : Editor
    {
        private static List<Type> dataCompTypes = new List<Type>();

        private WeaponDataSO dataSO;

        private bool showForceUpdateButtons;
        private bool showAddComponentsButtons;

        private void OnEnable()
        {
            dataSO = target as WeaponDataSO;
        }


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Set Number Of Attacks"))
            {
                foreach (var items in dataSO.ComponentData)
                {
                    items.InitializeAttackData(dataSO.NumberOfAttacks);
                }
            }

            showAddComponentsButtons = EditorGUILayout.Foldout(showAddComponentsButtons, "Add Components");

            if (showAddComponentsButtons)
            {
                foreach (var dataCompType in dataCompTypes)
                {
                    if (GUILayout.Button(dataCompType.Name))
                    {
                        var comp = Activator.CreateInstance(dataCompType) as ComponentData;

                        if (comp == null)
                            return;

                        comp.InitializeAttackData(dataSO.NumberOfAttacks);

                        dataSO.AddData(comp);
                    }
                }
            }

            showForceUpdateButtons = EditorGUILayout.Foldout(showForceUpdateButtons, "Force Update Buttons");

            if (showForceUpdateButtons)
            {
                if (GUILayout.Button("Force Update Component Names"))
                {
                    foreach (var item in dataSO.ComponentData)
                    {
                        item.SetComponentName();
                    }
                }

                if (GUILayout.Button("Force Update Attack Names"))
                {
                    foreach (var item in dataSO.ComponentData)
                    {
                        item.SetAttackDataNames();
                    }
                }
            }           
        }

        [DidReloadScripts]
        private static void OnRecompile()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            var filteredTypes = types.Where(type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass);
            dataCompTypes = filteredTypes.ToList();
        }
    }
}
