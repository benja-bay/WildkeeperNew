using UnityEngine;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyConfig enemyConfig;
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private Vector3 spawnOffset = Vector3.zero;

        private IEnemyFactory _factory;

        private void Awake()
        {
            _factory = new EnemyFactory();
        }

        private void Start()
        {
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            Vector3 spawnPosition = transform.position + spawnOffset;
            _factory.CreateEnemy(enemyConfig, spawnPosition, patrolPoints);
        }
    }
}
