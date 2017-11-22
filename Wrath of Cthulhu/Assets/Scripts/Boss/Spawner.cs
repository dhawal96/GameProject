using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float waveCount = 0f;
    public float spawnTime = 0f;
    public GameObject boss;
    //The amount of time between each spawn.
    public float spawnDelay = 0f;
    //The amount of time before spawning starts.
    public GameObject[] enemies;
    //Array of enemy prefabs.
    public Vector3 enposition;

    void OnEnable()
    {
        //Start calling the Spawn function repeatedly after a delay.
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
 
    }

    void Spawn()
    {
        //Randomly Spawn
        //float spawnPointX = Random.Range(2.63f, 48.58f);
        //float spawnPointY = Random.Range(1.51f, 5.78f);
        //Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, 0);

        waveCount++;
        //Instantiate a random enemy.
        int enemyIndex = Random.Range(0, enemies.Length);
        GameObject enemy = Instantiate(enemies[enemyIndex], gameObject.transform.position, transform.rotation);
        enemy.GetComponent<EnemyMove>().cameFromSpawner = true;

        if (waveCount == 3f)
        {
            CancelInvoke();
        }
    }
}
