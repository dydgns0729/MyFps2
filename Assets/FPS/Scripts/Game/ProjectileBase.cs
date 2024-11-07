using UnityEngine;
using UnityEngine.Events;

namespace Unity.FPS.Game
{
    /// <summary>
    /// 발사체의 기본이 되는 부모 클래스
    /// </summary>
    public abstract class ProjectileBase : MonoBehaviour
    {
        #region Variables
        public GameObject Owner { get; private set; }       //발사한 주체
        public Vector3 InitialPosition { get; private set; }    //초기 Position 값
        public Vector3 InitialDirection { get; private set; }   //초기 방향 값
        public Vector3 InheritedMuzzleVelocity { get; private set; }    //총구의 속도
        public float InitialCharge { get; private set; }    //초기 Charge값

        public UnityAction OnShoot;                         //발사시 등록된 함수 호출
        #endregion

        public void Shoot(WeaponController controller)
        {
            //초기값 설정
            Owner = controller.Owner;
            InitialPosition = this.transform.position;
            InitialDirection = this.transform.forward;
            InheritedMuzzleVelocity = controller.MuzzleWorldVelocity;
            InitialCharge = controller.CurrentCharge;

            OnShoot?.Invoke();
        }


    }
}