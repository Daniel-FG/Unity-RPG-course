using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth = 100f;  //最大血量
    [SerializeField] private float detectRadius = 5f;  //玩家進到此偵測範圍內會追逐玩家
    [SerializeField] private float attackRadius = 5f;  //玩家進到此範圍會射擊玩家
    [SerializeField] private float damagePerShot = 9f;  //每顆子彈的殺傷力
    [SerializeField] private float secondsPerShot = 0.5f;  //重複射擊的時間間隔
    [SerializeField] private Vector3 aimOffset = new Vector3(0f, 1f, 0f);  //瞄準方位補償
    [SerializeField] private GameObject projectileToUse;  //子彈
    [SerializeField] private GameObject turrent;  //發射子彈的物件

    private AICharacterControl aiCharacterControl;
    private GameObject player;
    private ThirdPersonCharacter thirdPersonControl = null;
    private bool isAttacking = false;
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

        if (distanceToPlayer <= attackRadius && !isAttacking)
        {
            isAttacking = true;
            InvokeRepeating("ShootProjectile", 0f, secondsPerShot);
        }

        if (distanceToPlayer > attackRadius)
        {
            isAttacking = false;
            CancelInvoke();
        }
    }

    private void ShootProjectile()
    {
        GameObject newProjectile = Instantiate(projectileToUse, turrent.transform.position, Quaternion.identity);  //產生子彈
        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();  //取得子彈的Projectile Component
        projectileComponent.SetDamage(damagePerShot);  //設定子彈傷害

        Vector3 attackDirection = (player.transform.position - turrent.transform.position + aimOffset).normalized;  //取得射擊方向
        projectileComponent.GetComponent<Rigidbody>().velocity = attackDirection * projectileComponent.projectileSpeed;  //設定射擊速度

        //TODO Destory fired projectiles
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
