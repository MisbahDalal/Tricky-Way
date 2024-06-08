using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private Camera myCam;
    private Vector3 screenPos;
    private float angleOffset;
    private Collider2D col;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    void Start()
    {
        myCam = Camera.main;
        col = GetComponent<Collider2D>();
        GetInitialRotations();
    }

    private void Update()
    {
        Vector3 mousePos = myCam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (col == Physics2D.OverlapPoint(mousePos))
            {
                screenPos = myCam.WorldToScreenPoint(transform.position);
                Vector3 vec3 = Input.mousePosition - screenPos;
                angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (col == Physics2D.OverlapPoint(mousePos))
            {
                Vector3 vec3 = Input.mousePosition - screenPos;
                float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
            }
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