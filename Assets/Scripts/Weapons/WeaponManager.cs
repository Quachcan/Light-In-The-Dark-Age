using Configs;
using UnityEngine;

namespace Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        public GameObject currentWeaponModel;

        private void UnLoadAndDestroyWeapons()
        {
            if (currentWeaponModel != null)
            {
                Destroy(currentWeaponModel);
            }
        }

        public void LoadWeaponsModel(WeaponItem weaponItem)
        {
            //UnLoadAndDestroyWeapons();

            if (weaponItem == null)
            {
                return;
            }
        
            GameObject weaponModel = Instantiate(weaponItem.itemModel, transform);
            weaponModel.transform.localPosition = Vector3.zero;
            weaponModel.transform.localRotation = Quaternion.identity;
            weaponModel.transform.localScale = Vector3.one;
            currentWeaponModel = weaponModel;
        }
    }
}
