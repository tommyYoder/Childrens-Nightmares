using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class EnemyAttack : MonoBehaviour
    {
        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 10;


        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        bool playerInRange;
        float timer;


        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");             // Looks for Player.
            playerHealth = player.GetComponent<PlayerHealth>();             // Checks for player health.
            enemyHealth = GetComponent<EnemyHealth>();                     // Checks for enemy health.
            anim = GetComponent<Animator>();                              // Gets the animator component. 
        }


        void OnTriggerEnter(Collider other)                              // If player tag equals enemy tag, then PlayerInRange is set to true.
        {
            if (other.gameObject == player)
            {
                playerInRange = true;
            }
        }


        void OnTriggerExit(Collider other)                           // If player tag doesn't equal enemy tag, then PlayerInRange is set to false.
        {
            if (other.gameObject == player)
            {
                playerInRange = false;
            }
        }


        void Update()
        {
            timer += Time.deltaTime;                                 // Updates each attack by a timer. If enemy is within range and health is greater than zero attack is true.

            if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            {
                Attack();
            }

            if (playerHealth.currentHealth <= 0)                   // If health is less then 0, then Player Animator is set to true, and attack is set to false. 
            {
                anim.SetTrigger("PlayerDead");
            }
        }


        void Attack()
        {
            timer = 0f;

            if (playerHealth.currentHealth > 0)                   // Timer allows enemy to attack and player to recieve damage if health is greater than 0.
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}
