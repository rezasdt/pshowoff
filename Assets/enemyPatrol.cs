using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Transform targetPoint;
    private bool facingRight = true;

    void Start()
    {
        targetPoint = pointA;
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        // Move towards the target point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Check if we have reached the target point
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Switch target point
            if (targetPoint == pointA)
            {
                targetPoint = pointB;
            }
            else
            {
                targetPoint = pointA;
            }

            // Flip the character
            Flip();
        }
    }

    void Flip()
    {
        // Determine the direction to the target point and flip if necessary
        if ((targetPoint.position.x > transform.position.x && !facingRight) || (targetPoint.position.x < transform.position.x && facingRight))
        {
            facingRight = !facingRight;

            // Rotate the character 180 degrees around the y-axis
            transform.Rotate(0f, 180f, 0f);

            // Log the flipping action for debugging
            //Debug.Log("Character flipped. New rotation: " + transform.rotation.eulerAngles);
        }
    }
}