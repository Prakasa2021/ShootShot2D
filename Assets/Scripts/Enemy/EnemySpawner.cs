using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPref;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
    [SerializeField] int posX;
    [SerializeField] int posY;
    [SerializeField] int randomY1;
    [SerializeField] int randomY2;
    [SerializeField] bool isRandom;
    private float timeTilSpawn;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        timeTilSpawn -= Time.deltaTime;

        if (gameManager.timeIsRunning)
        {
            if (timeTilSpawn <= 0)
            {
                if (!isRandom)
                {
                    Instantiate(enemyPref, new Vector3(posX, posY, 0), Quaternion.identity);
                    SetTimeUntilSpawn();
                }
                else
                {
                    Instantiate(enemyPref, new Vector3(posX, Random.Range(randomY1, randomY2), 0), Quaternion.identity);
                    SetTimeUntilSpawn();
                }
            }
        }
    }

    void SetTimeUntilSpawn()
    {
        timeTilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
