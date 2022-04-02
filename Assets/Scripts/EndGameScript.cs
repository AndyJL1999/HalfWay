using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    public Animator anim;
    public GameObject victoryText;
    bool inPlace;

    private void Start()
    {
        victoryText.SetActive(false);
        inPlace = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && inPlace == true)
        {
            anim.SetTrigger("FadeOut");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        victoryText.SetActive(true);
        inPlace = true;
    }

    private void OnTriggerExit(Collider other)
    {
        victoryText.SetActive(false);
        inPlace = false;
    }
}
