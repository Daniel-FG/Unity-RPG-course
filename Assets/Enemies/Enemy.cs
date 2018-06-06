using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;  //最大血量
    [SerializeField] private float detectRadius = 5f;  //玩家進到此偵測範圍內會追逐玩家

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
    }
}
