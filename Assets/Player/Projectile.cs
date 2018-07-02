using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damageCaused = 10f;
    private void OnTriggerEnter(Collider other)
    {
        Component damagableComponent = other.gameObject.GetComponent(typeof(IDamagable));
        if (damagableComponent)
        {
            (damagableComponent as IDamagable).TakeDamage(damageCaused);
        }
    }
}
