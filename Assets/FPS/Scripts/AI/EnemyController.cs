using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.AI
{
    public class EnemyController : MonoBehaviour
    {
        #region Variables
        private Health health;

        //death
        public GameObject deathVfxPrefab;
        public Transform deathVfxSpawnPostion;
        #endregion

        private void Start()
        {
            //참조
            health = GetComponent<Health>();
            health.OnDamaged += OnDamaged;
            health.OnDie += OnDie;
        }

        private void OnDamaged(float damage, GameObject damageSource)
        {

        }

        private void OnDie()
        {
            GameObject effectGo = Instantiate(deathVfxPrefab, deathVfxSpawnPostion.position, Quaternion.identity);
            Destroy(effectGo, 5f);
        }


    }
}