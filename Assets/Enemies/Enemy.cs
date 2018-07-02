using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamagable
{
    public float detectRadius = 5f;  //玩家進到此偵測範圍內會追逐玩家
    public float attackRadius = 5f;

    [SerializeField] private float maxHealth = 100f;  //最大血量

    private AICharacterControl aiCharacterControl;
    private GameObject player;
    private ThirdPersonCharacter thirdPersonControl = null;
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
        player = GameObject.FindGameObjectWithTag("Player");
        aiCharacterControl = GetComponent<AICharacterControl>();
        thirdPersonControl = GetComponent<ThirdPersonCharacter>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if(distanceToPlayer <= detectRadius)
        {
            aiCharacterControl.SetTarget(player.transform);
        }
        else
        {
            aiCharacterControl.SetTarget(transform);
        }

        if (distanceToPlayer <= attackRadius)
        {
            print(name + " attacking player");
            //TODO instantiate projectile
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
    }

    private void OnDrawGizmos()  //在Game Scene中把Gizmo打開之後每禎呼叫
    {
        Gizmos.color = Color.blue;  //設定顏色
        Gizmos.DrawWireSphere(transform.position, detectRadius);  //在該點畫半徑為0.1f的空心圓形

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
