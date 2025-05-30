// #TEST
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

        public void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            // direcciond el player
            var playerDirection = other.transform.position - transform.position;
            // layer 
            var layer = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("World");
            // raycast 
            Debug.DrawRay(transform.position, playerDirection.normalized * _circleCollider2D.radius * 1.1f, Color.red, 1f);
            var rayCast = Physics2D.Raycast(transform.position, playerDirection, _circleCollider2D.radius * 1.1f, layer);
            
            if (rayCast.collider == null) return;

            if (rayCast.collider.CompareTag("Player"))
            {
                _enemyController.SetTarget(other.transform);
            }
            
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            _enemyController.SetTarget(null);
        }
    }
}
