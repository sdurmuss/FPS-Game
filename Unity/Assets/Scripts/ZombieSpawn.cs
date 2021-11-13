using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    public static ZombieSpawn zombieSpawn;
    float zombieSpawnTimer = 3f;
    float appleSpawnTimer = 30f;
    float ak47SpawnTimer = 30f;
    int ak11, ak22, ak33, ak44, a11, a22, a33, a44;
    public GameObject zombiePrefab, applePrefab, ak47Prefab;
    public Transform z1, z2, ak1, ak2, ak3, ak4, a1, a2, a3, a4;
    private void Start()
    {
        zombieSpawn = this;
    }
    void Update()
    {
        if (zombieSpawnTimer <= 0)
        {
            int value = Random.Range(1, 3);
            switch (value)
            {
                case 1:
                    Instantiate(zombiePrefab, z1.position, Quaternion.identity);         
                    break;
                case 2:
                    Instantiate(zombiePrefab, z2.position, Quaternion.identity);
                    break;
            }
            zombieSpawnTimer = 3f;
        }                                            

        if(appleSpawnTimer <= 0)
        {
            int value = Random.Range(1, 5);
            switch (value)
            {
                case 1:
                    Instantiate(applePrefab, a1.position, Quaternion.identity);
                    a11 = 1;
                    break;
                case 2:
                    Instantiate(applePrefab, a2.position, Quaternion.identity);
                    a22 = 1;
                    break;
                case 3:
                    Instantiate(applePrefab, a3.position, Quaternion.identity);
                    a33 = 1;
                    break;
                case 4:
                    Instantiate(applePrefab, a4.position, Quaternion.identity);
                    a44 = 1;
                    break;
            }
            appleSpawnTimer = 30f;
        }

        if (ak47SpawnTimer <= 0)
        {
            int value = Random.Range(1, 5);
            switch (value)
            {
                case 1:
                    Instantiate(ak47Prefab, ak1.position, Quaternion.identity);
                    ak11 = 1;
                    break;
                case 2:
                    Instantiate(ak47Prefab, ak2.position, Quaternion.identity);
                    ak22 = 1;
                    break;
                case 3:
                    Instantiate(ak47Prefab, ak3.position, Quaternion.identity);
                    ak33 = 1;
                    break;
                case 4:
                    Instantiate(ak47Prefab, ak4.position, Quaternion.identity);
                    ak44 = 1;
                    break;
            }
            ak47SpawnTimer = 30f;
        }

        zombieSpawnTimer -= Time.deltaTime;
        appleSpawnTimer -= Time.deltaTime;
        ak47SpawnTimer -= Time.deltaTime;

    }
}
