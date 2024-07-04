using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPref;
    [SerializeField] int currentEnemySpawn;
    [SerializeField] int targetEnemySpawn;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
    [SerializeField] float posX;
    [SerializeField] float posY;
    [SerializeField] float randomY1;
    [SerializeField] float randomY2;
    [SerializeField] bool isRandomPos;
    private float timeUntilSpawn;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        SetTimeSpawn();
    }

    void SetTimeSpawn()
    {
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.timeIsRunning)
        {
            timeUntilSpawn -= Time.deltaTime;

            if (timeUntilSpawn <= 0)
            {
                SpawnEnemy();
                SetTimeSpawn();
            }
        }
        else
            currentEnemySpawn = 0;
    }

    void SpawnEnemy()
    {
        if (currentEnemySpawn < targetEnemySpawn)
        {
            if (!isRandomPos)
            {
                Instantiate(enemyPref, new Vector3(posX, posY, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(enemyPref, new Vector3(posX, Random.Range(randomY1, randomY2), 0), Quaternion.identity);
            }
            currentEnemySpawn += 1;
        }
    }
}
