using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int ballLives;
    public TextMeshProUGUI ballLivesText;

    public float levelTime = 60f; // Total time for the level
    private float currentTime;

    public Image timerBar;

    public GameObject ball;
    public GameObject levelCompleteUI;
    public GameObject tryAgainUI;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists
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

    private void Start()
    {
        currentTime = levelTime;
        UpdateLivesText();
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerBar();

            if (currentTime <= 0)
            {
                TimeUp();
            }
        }
    }

    private void UpdateTimerBar()
    {
        if (timerBar != null)
        {
            // Assuming the fill amount of the image represents the remaining time
            timerBar.fillAmount = Mathf.Clamp(currentTime / levelTime, 0, 1);
        }
    }

    private void UpdateLivesText()
    {
        //if (ballLives != 0)
        //{
            ballLivesText.text = ballLives.ToString();
        //}
    }

    public void LoseLife()
    {
        ballLives--;
        UpdateLivesText();
        if (ballLives > 0)
        {
            Debug.Log("Balls Left: " +  ballLives);
            if (ball != null)
            {
                ball.GetComponent<BallController>().ResetPosition();
            }
            ResetLevel();
            //ReloadCurrentScene();
        }
        else
        {
            // Add Game Over later
            //Debug.Log("Game Over");
            OutOfBalls();
        }
    }

    //private void ReloadCurrentScene()
    //{
    //    string currentSceneName = SceneManager.GetActiveScene().name;
    //    SceneManager.LoadScene(currentSceneName);
    //}

    private void TimeUp()
    {
        // Display Try Again UI
        tryAgainUI.SetActive(true);
        // Stop game
        Time.timeScale = 0f;
    }

    private void OutOfBalls()
    {
        // Display Try Again UI
        Debug.Log("Out of lives");
        tryAgainUI.SetActive(true);
        // Stop game
        Time.timeScale = 0f;
    }

    public void CompleteLevel()
    {
        int stars = CalculateStars();
        // Display Level Complete UI
        levelCompleteUI.SetActive(true);
        // Show stars in UI
        levelCompleteUI.GetComponent<LevelCompleteUI>().DisplayStars(stars);
        // Stop game
        Time.timeScale = 0f;
    }

    private int CalculateStars()
    {
        if (currentTime > 40)
        {
            return 3;
        }
        else if (currentTime > 20)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    private void ResetLevel()
    {
        Debug.Log("Reset Level called");
        // Call ResetObstacles method on all GameObjects with the ObstacleController script
        ObstacleController[] obstacleControllers = FindObjectsOfType<ObstacleController>();
        foreach (ObstacleController obstacleController in obstacleControllers)
        {
            obstacleController.ResetObstacles();
        }

        // Reset the timer bar
        UpdateTimerBar();
    }

    public void RetryLevel()
    {
        // Reload the current scene
        Debug.Log("Restarting the level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // Reset time scale
    }

    public void GoToLevelsScreen()
    {
        // Load the levels screen scene
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            Time.timeScale = 1f; // Reset time scale
        }
        else
        {
            Debug.Log("No more levels to load!");
            // Optionally load a "Game Complete" screen
        }
    }
}
