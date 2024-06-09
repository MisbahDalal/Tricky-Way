using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public GameManage gameManage;
    private Vector3 initialPosition;
    private SpriteRenderer spriteRenderer;
    public Sprite frownBall;
    private Sprite happyBall;
    private float initialGravity;
    private Vector2 initialVelocity;
    private float initialAngularVelocity;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        getInitialVelocity();
        spriteRenderer = GetComponent<SpriteRenderer>();
        happyBall = spriteRenderer.sprite;
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
            audioManager.PlaySFX(audioManager.death);
            spriteRenderer.sprite = frownBall;
            stopBall();
            gameManage.LoseLife();
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            stopBall();
            audioManager.PlaySFX(audioManager.goal);
            audioManager.PlaySFX(audioManager.levelComplete);
            gameManage.LevelComplete();
        }
        else if (collision.gameObject.CompareTag("Walls"))
        {
            audioManager.PlaySFX(audioManager.wallTouch);
        }
    }
    public void ResetBall()
    {
        Debug.Log("Reset Ball called");
        spriteRenderer.sprite = happyBall;
        transform.position = initialPosition;
        setInitialVelocity();
    }

    public void stopBall()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void getInitialVelocity()
    {
        initialGravity = GetComponent<Rigidbody2D>().gravityScale;
        initialVelocity = GetComponent<Rigidbody2D>().velocity;
        initialAngularVelocity = GetComponent<Rigidbody2D>().angularVelocity;
    }

    public void setInitialVelocity()
    {
        GetComponent<Rigidbody2D>().velocity = initialVelocity;
        GetComponent<Rigidbody2D>().angularVelocity = initialAngularVelocity;
        GetComponent<Rigidbody2D>().gravityScale = initialGravity;
    }

}
