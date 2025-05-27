using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Lifetime & Speed")]
    [SerializeField] private float _lifeTime = 3f;
    [SerializeField] private float _bulletSpeed = 10f;

    [Header("Damage")]
    [SerializeField] private int _damage = 20;    
    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * _bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            return;

        if (other.CompareTag("Enemy"))
        {
            
            var health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(_damage);
            }
            
            Destroy(gameObject);
        }
        else if (other.isTrigger == false)
        {
            // destruir la bala si golpea cualquier muro/obstáculo no trigger
            Destroy(gameObject);
        }
    }
}
