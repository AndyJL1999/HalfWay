using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandGunScript : MonoBehaviour
{
    Animator  anim;

    private int maxAmmo = 20;
    private int currentAmmo;

    public static bool hasSecondary;

    public Text ammoText;
    public AudioSource walkSound;
    public GameObject reloadPrompt;
    public GameObject secondary;
    public float timeBetweenBullets = 0.2f;
    public float range = 50f;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;
    AudioSource gunAudio;
    float effectsDisplayTime = 0.2f;
    Vector3 lastPos;
    bool canReload;
    bool reloading;
    float reloadTimer = 2f;
    int damagePerShot = 15;

    void Start()
    {
        currentAmmo = maxAmmo;
        ammoText.text = $"{currentAmmo}/{maxAmmo}";
        shootableMask = LayerMask.GetMask("Shootable");
        anim = GetComponent<Animator>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        reloadPrompt.SetActive(false);
        lastPos = transform.position;
        hasSecondary = false;
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (hasSecondary)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                gameObject.SetActive(false);
                secondary.SetActive(true);
            }
        }

        if (CheckForMove())
        {
            anim.SetBool("Walk", true);

            if(walkSound.isPlaying == false)
                walkSound.Play();
        }
        else
        {
            anim.SetBool("Walk", false);
            walkSound.Stop();
        }

        if (Input.GetButtonDown("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && currentAmmo > 0)
        {
            if (reloading == false)
            {
                Shoot();
                anim.SetBool("Aim", false);
            }
        }
        else if (currentAmmo < maxAmmo)
        {
            if(currentAmmo == 0)
            {
                if (gameObject.activeInHierarchy)
                    reloadPrompt.SetActive(true);
                else
                    reloadPrompt.SetActive(false);
            }
            canReload = true;
            anim.SetBool("Full Ammo", false);
        }

        if (canReload)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                anim.SetTrigger("Reload");
                reloading = true;
            }

            if(reloading)
                reloadTimer -= Time.deltaTime;

            if (reloadTimer <= 0)
            {
                currentAmmo = maxAmmo;
                canReload = false;
                reloading = false;
                reloadPrompt.SetActive(false);
                reloadTimer = 2f;
                anim.SetBool("Full Ammo", true);
            }
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
        ammoText.text = $"{currentAmmo}/{maxAmmo}";
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;

        anim.SetTrigger("Shoot");

        currentAmmo--;

        gunAudio.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    bool CheckForMove()
    {
        var displacement = transform.position - lastPos;
        lastPos = transform.position;

        return displacement.magnitude > 0.005;
    }
}

