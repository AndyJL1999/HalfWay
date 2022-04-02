using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 40;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;

    bool playerInRange;
    float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange)
        {
            Attack();
        }else if(playerInRange == false && playerHealth.currentHealth > 0)
        {
            anim.SetTrigger("Chase");
        }
    }

    void Attack()
    {
        timer = 0f;
        if(playerHealth.currentHealth > 0)
        {
            anim.SetTrigger("Attack");
            playerHealth.TakeDamage(attackDamage);
        }
        else
        {
            anim.SetTrigger("PlayerDead");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        playerInRange = false;
    }

}
