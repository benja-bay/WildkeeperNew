using System.Collections;
using System.Collections.Generic;
using Player;
using Systems;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthbarfill;
    private Health Health;
    private float _maxHealth;


    void Start()
    {
        Health = GameObject.Find("Player").GetComponent<Health>();
        _maxHealth = Health._currentHealth;
    }

    void Update()
    {
        healthbarfill.fillAmount = Health._currentHealth / _maxHealth;
    }
}
