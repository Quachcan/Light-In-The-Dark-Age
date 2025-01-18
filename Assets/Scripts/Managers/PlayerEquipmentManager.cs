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
        [SerializeField]
        private RightHandIKTarget _rightHandIKTarget;
        [SerializeField]
        private LeftHandIKTarget _leftHandIKTarget;
        //SubWeapon

        private void Awake()
        {
            _animationManager = GetComponent<AnimationManager>();
            LoadWeaponsManager();
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
           _rightHandIKTarget = _weaponManager.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();
            _leftHandIKTarget = _weaponManager.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
            _animationManager.AssignHandIK(_rightHandIKTarget, _leftHandIKTarget);
        }
        
    }
}
