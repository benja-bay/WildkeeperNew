using UnityEngine;

namespace Enemies
{
    // Sistema de visión del enemigo: detecta al jugador mediante colisiones y raycasts
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
            // Si el jugador está dentro del campo de visión y hay línea de visión directa, se convierte en objetivo
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
            // Si el jugador sale del campo de visión, se desasigna como objetivo
            if (other.CompareTag("Player"))
            {
                _enemyController.SetTarget(null);
            }
        }
    }
}
