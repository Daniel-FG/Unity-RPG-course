using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth = 100f;

    private float currentHealth = 100f;

    public float HealthAsPercentage
    {
        get
        {
            return currentHealth / maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
    }
}
