using Greg.Weapons.Components;
using UnityEngine;

namespace Greg.Weapons.Components
{
    public class MovementData : ComponentData<AttackMovement>
    {
        protected override void SetComponentDepenency()
        {
            ComponentDependency = typeof(Movement);
        }
    }
}
