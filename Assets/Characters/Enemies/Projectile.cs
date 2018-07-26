using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 10f;

    private float damageCaused = 10f;

    public void SetDamage(float damage)
    {
        damageCaused = damage;
    }
    private void OnCollisionEnter(Collision other)
    {
        Component damagableComponent = other.gameObject.GetComponent(typeof(IDamagable));
        if (damagableComponent)
        {
            (damagableComponent as IDamagable).TakeDamage(damageCaused);
        }
        Destroy(gameObject);
    }
}
