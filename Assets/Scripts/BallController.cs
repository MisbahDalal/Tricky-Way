using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public GameManage gameManage;
    private Vector3 initialPosition;

    void Start()
    {
        StoreInitialPosition();
    }

    private void StoreInitialPosition()
    {
        initialPosition = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Deadzone"))
        {
            gameManage.LoseLife();
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            gameManage.LevelComplete();
        }
    }
    public void ResetBall()
    {
        Debug.Log("Reset Ball called");
        transform.position = initialPosition;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

}
