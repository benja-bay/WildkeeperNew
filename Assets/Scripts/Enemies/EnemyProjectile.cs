using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int Damage;
    public float Speed;

    private Vector2 _direction;

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
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
        else if (other.CompareTag("Wall") || other.gameObject.layer == LayerMask.NameToLayer("World"))
        {
            Destroy(gameObject);
        }
    }
}
