using Greg.Weapons.Components.ComponentData.AttackData;
using UnityEngine;

namespace Greg.Weapons.Components.ComponentData
{
    public class MovementData : ComponentData
    {
        [field: SerializeField] public AttackMovement[] AttackData { get; private set; }    
    }
}
