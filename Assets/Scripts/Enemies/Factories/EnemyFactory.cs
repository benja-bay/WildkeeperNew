using UnityEngine;

namespace Enemies
{
    public class EnemyFactory : IEnemyFactory
    {
        public GameObject CreateEnemy(EnemyConfig config, Vector3 spawnPosition, Transform[] patrolPoints)
        {
            GameObject enemyInstance = Object.Instantiate(config.Prefab, spawnPosition, Quaternion.identity);

            EnemyController controller = enemyInstance.GetComponent<EnemyController>();
            if (controller == null)
            {
                Debug.LogError("Enemy prefab does not contain an EnemyController component.");
                return null;
            }
            
            EnemyVision vision = enemyInstance.GetComponent<EnemyVision>();
            if (vision != null)
            {
                vision.SetVisionRadius(config.VisionRadius);
            }

            controller.PatrolPoints = patrolPoints;
            controller.Initialize(config);

            return enemyInstance;
        }
    }
}
