using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
    public class PlayerHealth : MonoBehaviour
    {
        public int startingHealth = 100;
        public int currentHealth;
        public Slider healthSlider;
        public Image damageImage;
        public AudioClip deathClip;
        public float flashSpeed = 5f;
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

        public GameObject Health;

        public AudioSource HeartbeatFastSound;
        public AudioSource HeartbeatSlowSound;
        public AudioSource HealthSound;

        Animator anim;
        AudioSource playerAudio;
        PlayerMovement playerMovement;
        PlayerShooting playerShooting;
        bool isDead;
        bool damaged;


        void Awake()
        {
            anim = GetComponent<Animator>();                                              // Gets Animator.
            playerAudio = GetComponent<AudioSource>();                                   // Gets Audio Source.
            playerMovement = GetComponent<PlayerMovement>();                            // Gets player movement script.
            playerShooting = GetComponentInChildren<PlayerShooting>();                 // Gets Player Shooting script.
            currentHealth = startingHealth;                                           // Checks to make sure current health equals starting health.
            Health.GetComponent<BoxCollider>().isTrigger = false;                    // Health collider trigger is set to false.
            Health.SetActive(false);                                                // Health game object is set to false. 
            
        }


        void Update()
        {
            if (damaged)                                                       // If damadge is true, then damadge color will flash on the screen.
            {
                damageImage.color = flashColour;
            }
            else                                                              // If no damadge, then damadge ewuals false. 
            {
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }
            damaged = false;
        }
         void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Health")                          // If player tag collides with health tag, then player's health will go back to 100%. Sounds will play when collecting the item, and all other sounds will stop before game object is destroyed. 
            {
                currentHealth = startingHealth;
                healthSlider.value = currentHealth;
                HealthSound.Play();
                HeartbeatSlowSound.Stop();
                HeartbeatSlowSound.loop = false;
                HeartbeatFastSound.Stop();
                HeartbeatFastSound.loop = false;
                Health.GetComponent<BoxCollider>().isTrigger = false;
                Health.SetActive(false);
                Destroy(other.gameObject);
            }
        }


        public void TakeDamage(int amount)                                        // If damadge is true, then player's slider will update and audio source will play.
        {
            damaged = true;

            currentHealth -= amount;

            healthSlider.value = currentHealth;

            playerAudio.Play();

            if (currentHealth <= 0 && !isDead)                                 // If health is 0, then player death sequence will play.
            {
              
                Death();
            }
           
            if(currentHealth < 95)                                           // If health is less than 95, then health game object will be true. 
            { 
                Health.GetComponent<BoxCollider>().isTrigger = true;
                Health.SetActive(true);

            } 
            if(currentHealth > 51)                                           // If health is greater than 51, then heartbeat sound will stop.
            {
                HeartbeatSlowSound.Stop();
                HeartbeatSlowSound.loop = false;
                HeartbeatFastSound.Stop();
                HeartbeatFastSound.loop = false;
            }
            if(currentHealth < 50)                                        // If health less than 50, heartbeat sound will play.
            {
                HeartbeatSlowSound.Play();
                HeartbeatSlowSound.loop = true;

            }
            if (currentHealth < 25)                                     // If health is less than 25, then heartbeat sound will be replaced with a faster version.
            {
                HeartbeatSlowSound.Stop();
                HeartbeatSlowSound.loop = false;
                HeartbeatFastSound.Play();
                HeartbeatFastSound.loop = true;

            } 
            if(currentHealth < 1)                                      // If health is less than 1, then all heartbeat sounds will stop.
            {
                HeartbeatFastSound.Stop();
                HeartbeatFastSound.loop = false;
            }
        }


        void Death()                                                  // If isDead is true, then player shooting is disabled, Animator is played, death clip is played, audio source is played, and movement or shooting is false. 
        {
            isDead = true;

          

            playerShooting.DisableEffects();

            anim.SetTrigger("Die");

            playerAudio.clip = deathClip;
            playerAudio.Play();
         

            playerMovement.enabled = false;
            playerShooting.enabled = false;
        }


        public void RestartLevel()
        {
            Application.LoadLevel(Application.loadedLevel);           // Game Over manager will reload the level when game over timer is finished. 
        }
    }
}
