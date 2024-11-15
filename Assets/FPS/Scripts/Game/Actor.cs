using UnityEngine;

namespace Unity.FPS.Game
{
    /// <summary>
    /// 게임에 등장하는 Actor를 관리하는 클래스
    /// </summary>
    public class Actor : MonoBehaviour
    {
        #region Variables
        //소속 - 아군, 적군 구분
        public int affiliation;
        //조준점
        public Transform aimPoint;

        private ActorManager actorManager;
        #endregion

        private void Start()
        {
            //Actor 리스트에 추가(등록)
            actorManager = GameObject.FindObjectOfType<ActorManager>();
            //리스트에 포함되어 있는지 확인
            if (actorManager.Actors.Contains(this) == false)
            {
                actorManager.Actors.Add(this);
            }
        }

        private void OnDestroy()
        {
            //리스트에서 삭제
            if (actorManager)
            {
                actorManager.Actors.Remove(this);
            }
        }
    }
}