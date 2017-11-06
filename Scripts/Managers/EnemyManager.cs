using UnityEngine;


namespace CompleteProject
{
    public class EnemyManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;
        public GameObject enemy;
        //public int maxEnemy = 8;
        //public int enemyCount = 0;
        //public EnemyHealth enemyHealth;
       
        public float spawnTime = 3f;
        public Transform[] spawnPoints;
        public GameObject lightParticle;
        public AudioSource PoofSound;

        void Start()
        {
            InvokeRepeating("Spawn", spawnTime, spawnTime);                  // Will invoke Spawn by the spawn time.
           
        }

        
 
       public void Spawn()
        {
            if (playerHealth.currentHealth <= 0f)                           // If player health is 0, then Spawn is set to false. 
            {
                return;
            }
            // If player health is greater than 0, then spawn will instantiate enemies at a random spawnpoint in the game with particle effect. Sound is played for the poof cloud.
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);     
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            Instantiate(lightParticle, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            PoofSound.Play();
            //enemyCount++;
            //if(enemyCount >= maxEnemy)
            //{
            //    CancelInvoke("Spawn");
            //    StartCoroutine("Enemy");
            //}

        }
    }
}
