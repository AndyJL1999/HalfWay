using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    public static bool restrict;

    public GameObject interactionPrompt;
    bool interacting;

    void Start()
    {
        interactionPrompt.SetActive(false);
        interacting = false;
        restrict = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interacting == true && EnemyManager.enemies.Length == 0 && restrict == false)
        {
            if(gameObject.tag == "MainDoor")
            {
                restrict = true;
            }

            Destroy(gameObject);
            interactionPrompt.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            interactionPrompt.SetActive(true);
            if (EnemyManager.enemies.Length > 0)
                interactionPrompt.GetComponent<Text>().text = "You must defeat all enemies";
            else if(EnemyManager.enemies.Length == 0 && restrict == true)
                interactionPrompt.GetComponent<Text>().text = "Take the page";
            else
                interactionPrompt.GetComponent<Text>().text = "Press 'E' to open";

            interacting = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        interactionPrompt.SetActive(false);
        interacting = false;
    }
}
