using UnityEngine;
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
            shootableMask = LayerMask.GetMask("Shootable");                             // Gets layermask component.
            gunParticles = GetComponent<ParticleSystem>();                              // Gets particle effect.
            gunLine = GetComponent<LineRenderer>();                                    // Gets lineRenderer.
            gunAudio = GetComponent<AudioSource>();                                   // Gets AudioSource. 
            gunLight = GetComponent<Light>();                                        // Gets gun light game object. 
        }
    

        void Update()
        {
            timer += Time.deltaTime; 

            if (Input.GetButton("Fire1") && timer >= weapons[currentWeapon].fireRate && Time.timeScale != 0)           // If player hits the left mouse button and timer plus weapons current settings is greater than 0, then Shoot is true.
            {
                Shoot();
            }

            if (timer >= weapons[currentWeapon].fireRate * effectsDisplayTime)                                        // If less than timer, then shoots are set to false. 
            {
                DisableEffects();
            }
        }



        public void DisableEffects()                                                                                // Sets the gun light and gun line to false. 
        {
            gunLine.enabled = false;
            gunLight.enabled = false; 
        }


        void Shoot()
        {
            timer = 0f;                                             // If timer = o, then audio source player, light is true, partciles are true, gune line is true, and shootray is set to its transform position by the direction of the gun.

            gunAudio.Play();

            gunLight.enabled = true;

            gunParticles.Stop();
            gunParticles.Play();

            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, weapons[currentWeapon].range, shootableMask))                 // Raycasts each shot by the shootableMask's range. 
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();                              
                if (enemyHealth != null)                                                                             // If shot collides with enemy, then enemy takes damage based by the current weapon.
                {
                    enemyHealth.TakeDamage(weapons[currentWeapon].damage, shootHit.point);
                }
                gunLine.SetPosition(1, shootHit.point);
            }
            else                                                                                                  // If shot doesn't collide with enemy, then no damaged is recieved when colliding with other objects.
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * weapons[currentWeapon].range);
            }

        }
    }
}