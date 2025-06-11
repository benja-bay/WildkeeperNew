using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class EnemyVision : MonoBehaviour
    {
        [SerializeField] private EnemyController _enemyController;
        private CircleCollider2D _circleCollider2D;

        private void Awake()
        {
            _circleCollider2D = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            Vector2 direction = other.transform.position - transform.position;
            int layerMask = (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("World"));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, _circleCollider2D.radius, layerMask);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                if (_enemyController.Target != other.transform)
                {
                    _enemyController.SetTarget(other.transform);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player") && _enemyController.Target == other.transform)
            {
                _enemyController.SetTarget(null);
            }
        }

        public void SetVisionRadius(float radius)
        {
            _circleCollider2D.radius = radius;
            Debug.Log($"[EnemyVision] Set radius to: {_circleCollider2D.radius}");
        }
    }
}
