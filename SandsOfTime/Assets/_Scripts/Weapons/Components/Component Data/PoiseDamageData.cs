using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Greg.Weapons.Components
{
    public class PoiseDamageData : ComponentData<AttackPoiseDamage>
    {
        protected override void SetComponentDepenency()
        {
            ComponentDependency = typeof(PoiseDamage);
        }
    }
}
