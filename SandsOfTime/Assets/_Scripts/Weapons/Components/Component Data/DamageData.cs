using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Greg.Weapons.Components
{
    public class DamageData : ComponentData<AttackDamage>
    {
        protected override void SetComponentDepenency()
        {
            ComponentDependency = typeof(Damage);
        }
    }
}
