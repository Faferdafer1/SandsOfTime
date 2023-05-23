using System;
using UnityEngine;

namespace Greg.Weapons.Components
{
    [Serializable]
    public class AttackSprites : AttackData
    {
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
    }

}

