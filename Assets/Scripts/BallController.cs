using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        StoreInitialPosition();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void StoreInitialPosition()
    {
        initialPosition = transform.position;
    }
    public void ResetPosition()
    {
        Debug.Log("Reset Ball called");
        transform.position = initialPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Deadzone")
        {
            GameManager.Instance.LoseLife();
            //ballLives--;
            //if (ballLives > 0 )
            //{
            //    reloadCurrentScene();
            //    ballLivesText.text = ballLives.ToString();
            //}
            //else
            //{
            //    //Game Over
            //}
        }
        else if (collision.gameObject.tag == "Goal")
        {
            GameManager.Instance.CompleteLevel();
        }
    }
    //void reloadCurrentScene()
    //{
    //    string currentSceneName = SceneManager.GetActiveScene().name;

    //    SceneManager.LoadScene(currentSceneName);
    //}
}
