using Configs;
using UnityEngine;
using Weapons;

namespace Managers
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        [Header("Player Equipment")] 
        private AnimationManager _animationManager;
        private WeaponManager _weaponManager;
        public WeaponItem weapon;
        //SubWeapon

        private void Awake()
        {
            LoadWeaponsManager();
            _animationManager = GetComponent<AnimationManager>();
        }

        private void Start()
        {
            LoadCurrentWeapon();
        }

        private void LoadWeaponsManager()
        {
            _weaponManager = GetComponentInChildren<WeaponManager>();
        }

        private void LoadCurrentWeapon()
        {
            _weaponManager.LoadWeaponsModel(weapon);
            _animationManager.animator.runtimeAnimatorController = weapon.animatorOverrideController;
        }
        
    }
}
