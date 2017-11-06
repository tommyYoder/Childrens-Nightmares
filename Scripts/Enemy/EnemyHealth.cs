using UnityEngine;
using UnityEngine.UI;

namespace CompleteProject
{
    public class EnemyHealth : MonoBehaviour
    {
        public int startingHealth = 100;
        public int currentHealth;
        public float sinkSpeed = 2.5f;
        public int scoreValue = 10;
        public Slider healthSlider;
        public AudioClip deathClip;
        public GameObject LightParticle;

        public AudioClip poofClip;

        Animator anim;
        AudioSource enemyAudio;
        ParticleSystem hitParticles;
        CapsuleCollider capsuleCollider;
        bool isDead;
        bool isSinking;

       

        void Awake()
        {
            anim = GetComponent<Animator>();                                // Gets animator componnent
            enemyAudio = GetComponent<AudioSource>();                       // Gets audio source
            hitParticles = GetComponentInChildren<ParticleSystem>();        // Gets hit particle.
            capsuleCollider = GetComponent<CapsuleCollider>();              // Gets capsule collider. 
          

            currentHealth = startingHealth;                                // Checks current health to starting health. 
        }


        void Update()
        {
            if (isSinking)                                                 // If isSinking is true, then enemy will fall downward by the vector 3 axis. 
            {
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            }
        }


        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            if (isDead)                                                  // If isDead is true, enemy will not take anymore damadge.
                return;

            enemyAudio.Play();                                          // If false, enemy sound will play, healthsider will update, and hit particles will instantiate.
            healthSlider.gameObject.SetActive(true);
            currentHealth -= amount;
            healthSlider.value = currentHealth;

            hitParticles.transform.position = hitPoint;
            hitParticles.Play();

            if (currentHealth <= 0)                                     // If health is 0, then death sequence gets called. 
            {
                
                Death();
              
            }
        }

        // isDead is set to true, health sider is set to false, capsule collider trigger is set to true, animator Dead gets called and played along with audio source. 
        void Death()                                                  
        {
            isDead = true;
            healthSlider.gameObject.SetActive(false);
            capsuleCollider.isTrigger = true;

            anim.SetTrigger("Dead");

            enemyAudio.clip = deathClip;
            enemyAudio.Play();
          
            
        }

        // Nav Mesh is set to false, rigidbody isKinematic is set to true, isSinking is set to true, score is updated, particle effect gets called, sound plays for poof cloud, and enemy is destroyed after 2 seconds. 
        public void StartSinking()
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;           
            GetComponent<Rigidbody>().isKinematic = true;
            isSinking = true;
            enemyAudio.clip = poofClip;
            enemyAudio.Play();
            Instantiate(LightParticle, transform.position, Quaternion.identity);
            ScoreManager.score += scoreValue;
            Destroy(gameObject, 2f);
           
        }
    }
}
