using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
    }
    public override void Die()
    {
        base.Die();
    }
}
