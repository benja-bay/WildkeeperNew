using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private Transform _barrel;     // Posicion donde salen las balas
    [SerializeField] private GameObject _bullet;    // Proyectil
    [SerializeField] private float _fireRate;       // Cadencia de tiro

    private float _fireTimer;                       // Control de cadencia
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleShooting(); 
    }

    private void HandleShooting()
    {
       if (Input.GetMouseButtonDown(0) && CanShoot())
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        _fireTimer = Time.time + _fireRate;
        Instantiate( _bullet, _barrel.position, _barrel.rotation);
    }

    private bool CanShoot()
    {
        return Time.time > _fireTimer;
    }
}
