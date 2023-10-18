using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kai Gidwani
// 10/17/23
// Controls the movement of all objects

public class MovementController : MonoBehaviour
{
    private Vector3 objectPosition; // Initialized in Start() via transform
    [SerializeField] private float speed = 1f; // Set in the inspector
    [SerializeField] private bool wrap = false;

    private Vector3 velocity = Vector3.zero;

    // Declare camera values
    private Camera cam;
    private float height;
    private float width;

    // Direction object is facing, must be normalized
    [SerializeField] private Vector3 direction = new Vector3(0, 0, 0);
    internal Vector3 Direction
    {
        get { return direction; } // Provide it if needed
        set // Only set a normalized copy!
        {
            direction = value.normalized;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        // Set up position
        objectPosition = transform.position;

        // Initialize camera values
        cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        // Velocity is direction * speed * deltaTime 
        // (we don’t really even need the tmp variable)
        velocity = direction * speed * Time.deltaTime;


        // New position is current position + velocity
        objectPosition += velocity;

        if (wrap)
        {
            // Extra logic to adjust for wrapping...

            // If passing the left side
            if (objectPosition.x < -width)
            {
                // Wrap to the right side
                objectPosition.x = width;
            }
            // If passing the right side
            else if (objectPosition.x > width)
            {
                // Wrap to the left side
                objectPosition.x = -width;
            }

            // If passing the top
            if (objectPosition.y < -height)
            {
                // Wrap to the bottom
                objectPosition.y = height;
            }
            // If passing the bottom
            else if (objectPosition.y > height)
            {
                // Wrap to the top
                objectPosition.y = -height;
            }
        }

        // Draw the vehicle at that new position
        transform.position = objectPosition;

        /*
         * Commenting out code for rotating in case I want it later
         * But for now I do not want the player to rotate
         * 
        if (Direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
        */
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, velocity);
    }
}
