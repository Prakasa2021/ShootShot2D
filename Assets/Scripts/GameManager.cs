using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        roundInfo = 1;
    }

    void Start()
    {
        timeIsRunning = true;
        upgradeUI.SetActive(false);
        timeRemaining = setTimeRound;
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

    void ActiveSpawner(int idx)
    {
        var randomLow = Random.Range(0, 2);
        var randomMed = Random.Range(0, 2);
        var randomHigh = Random.Range(0, 2);

        if (idx < 5)
        {
            lowEnemySpawner[randomLow].SetActive(true);
        }
        else if (idx == 5)
        {
            // foreach (var lowEnemy in lowEnemySpawner)
            // {
            //     lowEnemy.SetActive(false);
            // }
            bossSpawner[0].SetActive(true);
        }
        else if (idx > 5)
        {
            bossSpawner[0].SetActive(false);

            foreach (var lowEnemy in lowEnemySpawner)
            {
                lowEnemy.SetActive(false);
            }

            lowEnemySpawner[randomLow].SetActive(true);
            mediumEnemySpawner[randomMed].SetActive(true);
        }
        else if (idx == 10)
        {
            bossSpawner[1].SetActive(false);
        }
        else if (idx > 10)
        {

        }
    }
}