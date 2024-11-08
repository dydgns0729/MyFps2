using UnityEngine;

namespace Unity.FPS.Game
{
    /// <summary>
    /// TimeSelfDestruct 부착한 게임 오브젝트는 지정된 시간에 킬
    /// </summary>
    public class TimeSelfDestruct : MonoBehaviour
    {
        public float lifeTime = 1f;
        private float spawnTime;        //생성될때의 시간

        private void Awake()
        {
            spawnTime = Time.time;
        }
        private void Update()
        {
            if ((spawnTime + lifeTime) <= Time.time)
            {
                Destroy(gameObject);
            }
        }
    }
}