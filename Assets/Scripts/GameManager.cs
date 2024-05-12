using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] TMP_Text gemsText;
    [SerializeField] public bool timeIsRunning;
    [SerializeField] int gemsCount;
    [SerializeField] float timeRemaining;
    [SerializeField] TMP_Text timeText;
    [SerializeField] Launcher launcher;
    [SerializeField] GameObject upgradeUI;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    void Start()
    {
        timeIsRunning = true;
        upgradeUI.SetActive(false);
    }

    void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeIsRunning = false;

                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    upgradeUI.SetActive(true);
                    launcher.enabled = false;
                }
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
}
