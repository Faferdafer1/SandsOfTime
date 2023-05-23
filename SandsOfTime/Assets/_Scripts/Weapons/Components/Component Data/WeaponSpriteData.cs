using Greg.Weapons.Components;
using UnityEngine;

namespace Greg.Weapons.Components
{
    public class WeaponSpriteData : ComponentData<AttackSprites>
    {
        protected override void SetComponentDepenency()
        {
            ComponentDependency = typeof(WeaponSprite);
        }
    }
}
