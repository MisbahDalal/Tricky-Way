using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 initialMousePosition;
    private Vector3 initialSpritePosition;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    void Start()
    {
        GetInitialRotations();
    }

    void Update()
    {
        // Check for mouse input
        if (Input.GetMouseButtonDown(0))
        {
            // Get initial positions when the mouse button is pressed
            initialMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            initialSpritePosition = transform.position;

            // Check if the mouse is over the sprite
            RaycastHit2D hit = Physics2D.Raycast(initialMousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // Rotate the sprite based on mouse movement
        if (isDragging)
        {
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = currentMousePosition - initialSpritePosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void GetInitialRotations()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Reset obstacles to their initial positions
    //public void ResetObstacles()
    //{
    //    Debug.Log("Reset Obstacles called");
    //    transform.rotation = initialObstacleRotation;
    //}

    public void ResetObstacle()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}