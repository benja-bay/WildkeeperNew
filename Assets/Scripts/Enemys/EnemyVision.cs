using UnityEngine;

namespace Enemys
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class EnemyVision : MonoBehaviour
    {
        [SerializeField] private EnemyController _enemyController;
        private CircleCollider2D _circleCollider2D;

        private void Start()
        {
            _circleCollider2D = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            var direction = other.transform.position - transform.position;
            int layer = (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("World"));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, _circleCollider2D.radius, layer);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                _enemyController.SetTarget(other.transform);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _enemyController.SetTarget(null);
            }
        }
    }
}
