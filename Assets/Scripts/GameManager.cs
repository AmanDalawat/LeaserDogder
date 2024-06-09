using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject winScreen;
    public GameObject loseScreen;
    private int currentWave = 0;
    private int totalWaves = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void WinGame()
    {
        Debug.Log("You Win!");
        winScreen.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void LoseGame()
    {
        Debug.Log("You Lose!");
        loseScreen.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartGame()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WaveCompleted()
    {
        currentWave++;
        if (currentWave >= totalWaves)
        {
            WinGame();
        }
        else
        {
            // Trigger the next wave in LaserSpawner
            FindObjectOfType<LaserSpawner>().StartNextWave();
        }
    }
}
