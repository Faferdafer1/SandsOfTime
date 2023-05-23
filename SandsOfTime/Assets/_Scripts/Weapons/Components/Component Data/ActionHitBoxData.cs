using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Greg.Weapons.Components
{
    public class ActionHitBoxData : ComponentData<AttackActionHitBox>
    {
        [field: SerializeField] public LayerMask DetectableLayers { get; private set; }

        protected override void SetComponentDepenency()
        {
            ComponentDependency = typeof(ActionHitBox);            
        }
    }
}
