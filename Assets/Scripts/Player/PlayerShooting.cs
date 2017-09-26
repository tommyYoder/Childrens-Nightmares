﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject
{

    public class PlayerShooting : MonoBehaviour
    {
        public int damagePerShot = 20;
        public float timeBetweenBullets = 0.15f;
        public float range = 100f;


        float timer;
        Ray shootRay = new Ray();
        RaycastHit shootHit;
        int shootableMask;
        ParticleSystem gunParticles;
        LineRenderer gunLine;
        AudioSource gunAudio;
        Light gunLight;
        float effectsDisplayTime = 0.2f;


        public WeaponObject[] weapons;
        public int currentWeapon = 0;

        void Awake()
        {
            shootableMask = LayerMask.GetMask("Shootable");
            gunParticles = GetComponent<ParticleSystem>();
            gunLine = GetComponent<LineRenderer>();
            gunAudio = GetComponent<AudioSource>();
            gunLight = GetComponent<Light>();
        }
    

        void Update()
        {
            timer += Time.deltaTime;

            if (Input.GetButton("Fire1") && timer >= weapons[currentWeapon].fireRate && Time.timeScale != 0)
            {
                Shoot();
            }

            if (timer >= weapons[currentWeapon].fireRate * effectsDisplayTime)
            {
                DisableEffects();
            }
        }



        public void DisableEffects()
        {
            gunLine.enabled = false;
            gunLight.enabled = false;
        }


        void Shoot()
        {
            timer = 0f;

            gunAudio.Play();

            gunLight.enabled = true;

            gunParticles.Stop();
            gunParticles.Play();

            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, weapons[currentWeapon].range, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(weapons[currentWeapon].damage, shootHit.point);
                }
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * weapons[currentWeapon].range);
            }

        }
    }
}