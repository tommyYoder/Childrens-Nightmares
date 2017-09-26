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
            anim = GetComponent<Animator>();
            playerAudio = GetComponent<AudioSource>();
            playerMovement = GetComponent<PlayerMovement>();
            playerShooting = GetComponentInChildren<PlayerShooting>();
            currentHealth = startingHealth;
            Health.GetComponent<BoxCollider>().isTrigger = false;
            Health.SetActive(false);
            
        }


        void Update()
        {
            if (damaged)
            {
                damageImage.color = flashColour;
            }
            else
            {
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }
            damaged = false;
        }
         void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Health")
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


        public void TakeDamage(int amount)
        {
            damaged = true;

            currentHealth -= amount;

            healthSlider.value = currentHealth;

            playerAudio.Play();

            if (currentHealth <= 0 && !isDead)
            {
              
                Death();
            }
           
            if(currentHealth < 95)
            {
                Health.GetComponent<BoxCollider>().isTrigger = true;
                Health.SetActive(true);

            }
            if(currentHealth > 51)
            {
                HeartbeatSlowSound.Stop();
                HeartbeatSlowSound.loop = false;
                HeartbeatFastSound.Stop();
                HeartbeatFastSound.loop = false;
            }
            if(currentHealth < 50)
            {
                HeartbeatSlowSound.Play();
                HeartbeatSlowSound.loop = true;

            }
            if (currentHealth < 25)
            {
                HeartbeatSlowSound.Stop();
                HeartbeatSlowSound.loop = false;
                HeartbeatFastSound.Play();
                HeartbeatFastSound.loop = true;

            }
            if(currentHealth < 1)
            {
                HeartbeatFastSound.Stop();
                HeartbeatFastSound.loop = false;
            }
        }


        void Death()
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
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
