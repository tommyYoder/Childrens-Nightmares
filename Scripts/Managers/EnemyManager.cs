using UnityEngine;


namespace CompleteProject
{
    public class EnemyManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;
        public GameObject enemy;
       
       
        public float spawnTime = 3f;
        public Transform[] spawnPoints;


        void Start()
        {
            InvokeRepeating("Spawn", spawnTime, spawnTime);                  // Will invoke Spawn by the spawn time.
        }


        void Spawn()
        {
            if (playerHealth.currentHealth <= 0f)                           // If player health is 0, then Spawn is set to false. 
            {
                return;
            }

            int spawnPointIndex = Random.Range(0, spawnPoints.Length);      // If player health is greater than 0, then spawn will instantiate enemies at a random spawnpoint in the game. 

            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
        
    }
}
