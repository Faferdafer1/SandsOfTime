using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Greg.Weapons.Components
{
    public class KnockBackData : ComponentData<AttackKnockBack>
    {
        protected override void SetComponentDepenency()
        {
            ComponentDependency = typeof(KnockBack);
        }
    }
}
