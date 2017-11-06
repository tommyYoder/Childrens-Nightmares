using UnityEngine;
using System.Collections;

namespace CompleteProject
{ 
public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;      // Looks to find the player in the game if player health and enemy health is greater than 0. Enemy uses a nav mesh component on where to walk in the game level.
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


        void Update()
        {
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)     // If healther is greater than 0, then enemy uses nav mesh to find the player.
            {
                nav.SetDestination(player.position);
            }
            else                                                                    // if health is less than 0, then nav mesh is set to false. 
            {
                nav.enabled = false;
            }
        }
    }
}
