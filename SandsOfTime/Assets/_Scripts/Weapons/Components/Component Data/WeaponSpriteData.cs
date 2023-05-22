using Greg.Weapons.Components.ComponentData.AttackData;
using UnityEngine;

namespace Greg.Weapons.Components.ComponentData
{
    public class WeaponSpriteData : ComponentData
    {
        [field: SerializeField] public AttackSprites[] AttackData { get; private set; }      
    }
}
