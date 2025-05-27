using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private Transform _barrel;     // Posicion donde salen las balas
    [SerializeField] private GameObject _bullet;    // Proyectil
    [SerializeField] private float _fireRate;       // Cadencia de tiro

    private float _fireTimer;                       // Control de cadencia
    

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Shoot()
    {
        _fireTimer = Time.time + _fireRate;
        Instantiate( _bullet, _barrel.position, _barrel.rotation);
    }

    public bool CanShoot()
    {
        return Time.time > _fireTimer;
    }
}
