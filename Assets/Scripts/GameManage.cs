using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    public int ballLives = 3;
    public float levelTime = 30f;
    public Image timerBar;
    //public TextMeshProUGUI livesText;
    public GameObject levelCompletePopup;
    public GameObject levelFailedPopup;
    private float timeRemaining;
    private bool isLevelComplete = false;
    public GameObject ball_3;
    public GameObject ball_2;
    public GameObject ball_1;
    public GameObject ball_0;

    private ObstacleController[] obstacles;

    private void Start()
    {
        ball_3.SetActive(true);
        obstacles = FindObjectsOfType<ObstacleController>();
        timeRemaining = levelTime;
        UpdateUI();
    }

    private void Update()
    {
        if (!isLevelComplete)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                LevelFailed();
            }
            UpdateUI();
        }
    }

    public void LoseLife()
    {
        ballLives--;
        ball_3.SetActive(false);
        ball_2.SetActive(false);
        ball_1.SetActive(false);
        ball_0.SetActive(false);
        if (ballLives > 0)
        {
            FindObjectOfType<BallController>().ResetBall();
            ResetObstacles();
            if (ballLives == 2)
            {
                ball_2.SetActive(true);
            }
            else if (ballLives == 1)
            {
                ball_1.SetActive(true);
            }
        }
        else
        {
            LevelFailed();
            ball_0.SetActive(true);
        }
        UpdateUI();
    }

    public void LevelComplete()
    {
        isLevelComplete = true;
        //HideGameObjects();
        levelCompletePopup.SetActive(true);
    }

    public void LevelFailed()
    {
        isLevelComplete = true;
        //HideGameObjects();
        levelFailedPopup.SetActive(true);
    }

    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateUI()
    {
        timerBar.fillAmount = Mathf.Clamp01(timeRemaining / levelTime);
        //livesText.text = "Lives: " + ballLives.ToString();
    }

    private void ResetObstacles()
    {
        foreach (ObstacleController obstacle in obstacles)
        {
            obstacle.ResetObstacle();
        }
    }

    private void HideGameObjects()
    {
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj != gameObject && !obj.CompareTag("UI") && !obj.CompareTag("MainCamera"))
            {
                obj.SetActive(false);
            }
        }
    }
}