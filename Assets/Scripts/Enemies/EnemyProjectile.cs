using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int Damage;
    public float Speed;
    private Vector2 _direction;

    // Tiempo máximo que el proyectil estará activo antes de autodestruirse
    [SerializeField] private float _lifetime = 3f;

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
    }

    void Start()
    {
        // Programar autodestrucción
        Destroy(gameObject, _lifetime);
    }

    void Update()
    {
        transform.Translate(_direction * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var health = other.GetComponent<Player.PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(Damage);
            }

            Destroy(gameObject);
        }
    }
}
