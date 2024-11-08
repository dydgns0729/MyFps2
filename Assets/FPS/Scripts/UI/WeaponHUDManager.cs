using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;

namespace Unity.FPS.UI
{
    public class WeaponHUDManager : MonoBehaviour
    {
        #region Variables
        public RectTransform ammoPanel;             //ammoCountUI 부모 오브젝트(WeaponHUDManager)
        public GameObject ammoCountPrefab;          //AmmoCounter 프리팹

        private PlayerWeaponsManager playerWeaponsManager;
        #endregion

        private void Awake()
        {
            //참조
            playerWeaponsManager = GameObject.FindObjectOfType<PlayerWeaponsManager>();
            playerWeaponsManager.OnAddedWeapon += AddWeapon;
            playerWeaponsManager.OnAddedWeapon += RemoveWeapon;
            playerWeaponsManager.OnSwitchToWeapon += SwitchWeapon;
        }

        void AddWeapon(WeaponController newWeapon, int weaponIndex)
        {
            GameObject ammoCountGo = Instantiate(ammoCountPrefab, ammoPanel);
            AmmoCount ammoCount = ammoCountGo.GetComponent<AmmoCount>();
            ammoCount.Initialize(newWeapon, weaponIndex);

        }

        //무기제거시 Ammo Ui 제거
        void RemoveWeapon(WeaponController oldWeapon, int weaponIndex)
        {

        }

        void SwitchWeapon(WeaponController weapon)
        {
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(ammoPanel);
        }
    }
}