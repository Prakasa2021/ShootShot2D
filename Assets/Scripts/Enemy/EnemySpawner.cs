using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPref;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
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
                Instantiate(enemyPref, transform.position, Quaternion.identity);
                SetTimeUntilSpawn();
            }
        }
    }

    void SetTimeUntilSpawn()
    {
        timeTilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
