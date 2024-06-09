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

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
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
            gameManage.LoseLife();
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;
            GetComponent<Rigidbody2D>().gravityScale = 0;
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
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

}
