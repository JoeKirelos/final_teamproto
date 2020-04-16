using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public enum SpawnState {spawning, waiting, counting };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject enemyTypeA;
        public GameObject enemyTypeB;
        public GameObject enemyTypeM;
        public GameObject enemyTypeBoss;
        public int count;
        public float rate;

    }

    public Wave[] waves;

    public int nextWave = 0;
    public int waveCounter = 0;

    public SpawnState state = SpawnState.counting;

    public float timeTweenWaves = 5f;
    public float waveCountdown = 0;

    private float searchingInterval = 1f;
    private void Start()
    {
        waveCountdown = timeTweenWaves;        
    }

    private void Update()
    {
        if (state == SpawnState.waiting)
        {
            if (!EnemiesFighting())
            {
                FollowingWave();
                return;
            }
            else
            {
                return;
            }

        }

       if(waveCountdown <= 0)
        {
            if (state != SpawnState.spawning)
            {
                StartCoroutine( SpawnWave ( waves [nextWave] ) );
            }
        } 
       else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    bool EnemiesFighting()
    {
        searchingInterval -= Time.deltaTime;
        if(searchingInterval <= 0)
        {
            searchingInterval = 1f;
            if 
                (GameObject.FindGameObjectWithTag("EnemyA") == null && 
                GameObject.FindGameObjectWithTag("EnemyB") == null && 
                GameObject.FindGameObjectWithTag("MiniBoss") == null &&
                GameObject.FindGameObjectWithTag("Boss") == null)
            {
                return false;
            }
        }
            return true;
    }

    void FollowingWave()
    {

        state = SpawnState.counting;
        waveCountdown = timeTweenWaves;

        if (waveCounter == 10)
        {
            waveCounter = 0;
            if (timeTweenWaves > 1)
            {
                timeTweenWaves -= 0.5f;
            }
        }
        else
        {
            waveCounter++;
        }
    }
    IEnumerator SpawnWave (Wave wave)
    {
        state = SpawnState.spawning;
        if (waveCounter == 10)
        {
            if(nextWave < 5)
            {
                nextWave++;
            }
            GameObject enemyToSpawn = wave.enemyTypeBoss;
            SpawnEnemy(enemyToSpawn);
        }
        else
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject enemyToSpawn;
                int percent = Random.Range(0, 100);
                if (percent >= 70)
                {
                    enemyToSpawn = wave.enemyTypeB;
                }
                else if (percent < 70 && percent >= 60)
                {
                    enemyToSpawn = wave.enemyTypeM;
                }
                else
                {
                    enemyToSpawn = wave.enemyTypeA;
                }
                SpawnEnemy(enemyToSpawn);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }
        state = SpawnState.waiting;
        yield break;
    }

    void SpawnEnemy(GameObject enemy )
    {
        int xRand = Random.Range(-53, 53);
        int yRand = Random.Range(-30, 30);
        Vector3 randomizer = new Vector3(xRand, yRand, 0);
        Instantiate(enemy, Vector3.zero + randomizer, Quaternion.identity);
    }
}
