using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionScript : MonoBehaviour
{
    PlayerHealth playerHealth;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
    }
 
    private void OnTriggerEnter(Collider other)
    {
        playerHealth = other.GetComponent<PlayerHealth>();

        if(other.gameObject.CompareTag("Player"))
        {
            if (gameObject.name == "Rifle")
            {
                HandGunScript.hasSecondary = true;
                Destroy(gameObject);
            }
            else if (gameObject.tag == "Page")
            {
                UI_Controller.pagesCollected++;
                EnemyManager.roundSet = false;
                DoorScript.restrict = false;
                Destroy(gameObject);
            }
            else if (gameObject.tag == "Health" && playerHealth.currentHealth < 100)
            {
                playerHealth.currentHealth += 20;
                if (playerHealth.currentHealth > 100)
                {
                    playerHealth.currentHealth = 100;
                }
                Destroy(gameObject);
            }
        }
    }
}
