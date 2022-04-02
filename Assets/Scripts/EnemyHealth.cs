using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;

    Animator anim;
    CapsuleCollider capsuleCollider;

    bool isDead;
    bool isSinking;

    void Awake()
    {
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }

        if(gameObject.tag == "Boss")
        {
            UnityEngine.UI.Slider bossSlider = GetComponentInChildren<UnityEngine.UI.Slider>();
            bossSlider.maxValue = startingHealth;
            bossSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Death();
            StartSInking();
        }
    }

    void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger("Dead");
    }

    public void StartSInking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }
}
