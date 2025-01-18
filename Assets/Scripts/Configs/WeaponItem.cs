using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Item/Weapon Item")]
    public class WeaponItem : ItemConfig
    {
        [Header("Weapon Animation")]
        public AnimatorOverrideController animatorOverrideController;
    }
}
