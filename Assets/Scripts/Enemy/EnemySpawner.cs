using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {     
        public float enemySpawnWaitTime;
        public GameObject[] EnemyPrefabs;
        [HideInInspector]
        public int enemiesLeft;
        public float enemyWaveWaitTime;
    }

    
    [SerializeField]
    private Transform enemySpawnPoints;
    

    public Wave[] waves;

    public int currentWaveIndex = 0;

    private bool canCountdown;
    public float countdown;
    // Start is called before the first frame update
    void Start()
    {
        canCountdown = true;
        for (int i = 0; i< waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].EnemyPrefabs.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canCountdown == true)
        {
            countdown -= Time.deltaTime;
        }


        if (countdown <= 0f)
        {
            canCountdown = false;
            countdown = waves[currentWaveIndex].enemyWaveWaitTime;
            
        }



        if (waves[currentWaveIndex].enemiesLeft == 0)
        {
            canCountdown = true;
            currentWaveIndex++; 
        }
    }

    private IEnumerator SpawnEnemy()
    {
        float positionX = Random.Range(-2f, 2f);
        float positionZ = Random.Range(-2f, 2f);
        if (currentWaveIndex < waves.Length)
        {

            for (int i = 0; i < waves[currentWaveIndex].EnemyPrefabs.Length; i++)
            {
                GameObject enemyPrefabClone = Instantiate(waves[currentWaveIndex].EnemyPrefabs[i],
                     enemySpawnPoints.transform.position = new Vector3
                     (positionX * enemySpawnPoints.transform.position.x, 2.5f, positionZ * enemySpawnPoints.transform.position.z),
                     Quaternion.identity);
                yield return new WaitForSeconds(waves[currentWaveIndex].enemySpawnWaitTime);
            }
            
        }
    }

   

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (countdown <= 0f)
            {              
                StartCoroutine(SpawnEnemy());
            }
            
        }
    }
}
