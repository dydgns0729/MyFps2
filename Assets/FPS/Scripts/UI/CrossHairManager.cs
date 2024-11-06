using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class CrossHairManager : MonoBehaviour
    {
        #region Variables
        public Image crosshairImage;                //Cross헤어 UI 이미지
        public Sprite nullCrosshairSprite;          //액티브한 무기가 없을때 보이는 크로스헤어 이미지

        private RectTransform crosshairRectTransform;

        private CrossHairData crosshairDefault;     //평상시, 기본
        private CrossHairData crosshairTarget;      //타겟팅 됐을때

        private CrossHairData crosshairCurrent;     //실질적으로 세팅되는 크로스헤어
        [SerializeField] private float crosshairUpdateShrpness = 5.0f;  //Lerp 변수

        private PlayerWeaponsManager weaponsManager;

        private bool wasPointingAtEnemy;
        #endregion

        private void Start()
        {
            //참조
            weaponsManager = GameObject.FindObjectOfType<PlayerWeaponsManager>();

            //액티브한 무기 크로스 헤어로 변경
            OnWeaponChanged(weaponsManager.GetActiveWeapon());

            weaponsManager.OnSwitchToWeapon += OnWeaponChanged;
        }

        private void Update()
        {
            UpdateCrosshairPointingAtEnemy(false);

            wasPointingAtEnemy = weaponsManager.IsPointingAtEnemy;
        }

        //크로스헤어 그리기
        void UpdateCrosshairPointingAtEnemy(bool force)
        {
            if (crosshairDefault.CrossHairSprite == null) return;
            //평상시? 타켓팅?
            if (weaponsManager.IsPointingAtEnemy && (force || !wasPointingAtEnemy))        //적을 포착한 순간
            {
                crosshairCurrent = crosshairTarget;
                crosshairImage.sprite = crosshairCurrent.CrossHairSprite;
                crosshairRectTransform.sizeDelta = crosshairCurrent.CrossHairSize * Vector2.one;
            }
            else if (!weaponsManager.IsPointingAtEnemy && (force || wasPointingAtEnemy))   //적을 놓친 순간
            {
                crosshairCurrent = crosshairDefault;
                crosshairImage.sprite = crosshairCurrent.CrossHairSprite;
                crosshairRectTransform.sizeDelta = crosshairCurrent.CrossHairSize * Vector2.one;
            }

            crosshairImage.color = Color.Lerp(crosshairImage.color, crosshairCurrent.CrossHairColor, crosshairUpdateShrpness * Time.deltaTime);
            crosshairRectTransform.sizeDelta = Mathf.Lerp(crosshairRectTransform.sizeDelta.x, crosshairCurrent.CrossHairSize, crosshairUpdateShrpness * Time.deltaTime) * Vector2.one;
        }

        //무기가 바뀔때마다 crosshairImage를 변경
        void OnWeaponChanged(WeaponController newWeapon)
        {
            if (newWeapon)
            {
                crosshairImage.enabled = true;
                crosshairRectTransform = crosshairImage.GetComponent<RectTransform>();
                //액티브 무기의 크로스헤어 정보 가져오기
                crosshairDefault = newWeapon.crosshairDefault;
                crosshairTarget = newWeapon.crosshairTargetInSight;
            }
            else
            {
                if (nullCrosshairSprite)
                {
                    crosshairImage.sprite = nullCrosshairSprite;
                }
                else
                {
                    crosshairImage.enabled = false;
                }
            }
            UpdateCrosshairPointingAtEnemy(true);
        }
    }
}