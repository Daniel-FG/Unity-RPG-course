using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private const int enemyLayer = 9;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxAttackRange;
    [SerializeField] private float minAttackDelay;

    private CameraRaycaster cameraRaycaster;
    private GameObject currentTarget;
    private float currentHealth = 100f;
    public float HealthAsPercentage
    {
        get
        {
            return currentHealth / maxHealth;
        }
    }

    private void Start()
    {
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
    }

    public void OnMouseClick(RaycastHit raycastHit, int layerHit)
    {
        //TODO add range limits and damage and lastAttackTime
        if (layerHit == enemyLayer)
        {
            GameObject enemy = raycastHit.collider.gameObject;
            print("clicked on " + enemy);


        }
    }
}
