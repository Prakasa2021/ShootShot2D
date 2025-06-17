using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] public bool timeIsRunning;
    [SerializeField] public int gemsCount;
    [SerializeField] public int roundInfo;
    [SerializeField] float setTimeRound;
    [SerializeField] float timeRemaining;
    [SerializeField] TMP_Text gemsText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text roundText;
    [SerializeField] Launcher launcher;
    [SerializeField] GameObject upgradeUI;
    [SerializeField] GameObject[] lowEnemySpawner;
    [SerializeField] GameObject[] mediumEnemySpawner;
    [SerializeField] GameObject[] highEnemySpawner;
    [SerializeField] GameObject[] bossSpawner;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        roundInfo = 0;
    }

    void Start()
    {
        upgradeUI.SetActive(false);
        timeRemaining = setTimeRound;
        timeIsRunning = true;
        NextRound();
    }

    void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0f;
                timeIsRunning = false;
            }
        }
        else
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && GameObject.FindGameObjectsWithTag("EnemyRanged").Length <= 0)
            {
                upgradeUI.SetActive(true);
                launcher.enabled = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float mins = Mathf.FloorToInt(timeToDisplay / 60);
        float secs = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00} : {1:00}", mins, secs);
    }

    private void OnGUI()
    {
        gemsText.text = gemsCount.ToString();
    }

    public void GemsCount(int gems)
    {
        gemsCount += gems;
    }

    public void UpgradeCost(int cost)
    {
        gemsCount -= cost;
    }

    public void NextRound()
    {
        roundInfo += 1;
        roundText.text = roundInfo.ToString();
        timeRemaining = setTimeRound;
        timeIsRunning = true;
        ActiveSpawner(roundInfo);
    }

    void ActiveSpawner(int round)
    {
        var randomLow = Random.Range(0, lowEnemySpawner.Length);
        var randomMed = Random.Range(0, mediumEnemySpawner.Length);
        var randomHigh = Random.Range(0, highEnemySpawner.Length);
        var randomBoss = Random.Range(0, bossSpawner.Length);

        if (round < 5)
        {
            lowEnemySpawner[randomLow].SetActive(true);
        }
        else if (round % 5 == 0)
        {
            bossSpawner[randomBoss].SetActive(true);
        }
        else if (round > 5)
        {
            mediumEnemySpawner[randomMed].SetActive(true);
        }
        else if (round > 10)
        {
            highEnemySpawner[randomHigh].SetActive(true);
        }
        else if (round > 20)
        {
            for (int i = 0; i < lowEnemySpawner.Length; i++)
            {
                lowEnemySpawner[i].SetActive(true);
                mediumEnemySpawner[i].SetActive(true);
                highEnemySpawner[i].SetActive(true);
            }
        }

        if (round % 5 != 0)
        {
            foreach (var boss in bossSpawner)
            {
                boss.SetActive(false);
            }
        }

        // if (round < 5)
        // {
        //     lowEnemySpawner[randomLow].SetActive(true);
        // }
        // else if (round == 5)
        // {
        //     // foreach (var lowEnemy in lowEnemySpawner)
        //     // {
        //     //     lowEnemy.SetActive(false);
        //     // }
        //     bossSpawner[0].SetActive(true);
        // }
        // else if (round > 5)
        // {
        //     bossSpawner[0].SetActive(false);

        //     foreach (var lowEnemy in lowEnemySpawner)
        //     {
        //         lowEnemy.SetActive(false);
        //     }

        //     lowEnemySpawner[randomLow].SetActive(true);
        //     mediumEnemySpawner[randomMed].SetActive(true);
        // }
        // else if (round == 10)
        // {
        //     bossSpawner[1].SetActive(false);
        // }
        // else if (round > 10)
        // {

        // }
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void EndGame()
    {
        timeIsRunning = false;
    }
}