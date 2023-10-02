using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
    // Info if circle
    [SerializeField] private float radius;
    [SerializeField] public float Radius { get { return radius; } }


    // Info if box
    [SerializeField] private Vector2 boundingBox = Vector2.one;
    public Vector2 BoxSize { get { return boundingBox; } }

    // Renderer to change color
    [SerializeField] SpriteRenderer spriteRenderer;


    // Properties for RectMin, RectMax, ... to return the minimum and maximum corners as Vector2's
    // Take into account sprite size AND transform.position
    public Vector2 RectMin
    { get { return new Vector2(transform.position.x - boundingBox.x / 2, transform.position.y - boundingBox.y / 2); } }

    public Vector2 RectMax
    { get { return new Vector2(transform.position.x + boundingBox.x / 2, transform.position.y + boundingBox.y / 2); } }


    // Flag to note if actively colliding
    [SerializeField] private bool isColliding = false;
    public bool IsColliding { get { return isColliding; } set { isColliding = value; } }

    // Update is called once per frame
    void Update()
    {
        if (isColliding)
        {
            spriteRenderer.color = Color.red;
        }
        else
        { 
            spriteRenderer.color = Color.white;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(
            transform.position,
            new Vector3(boundingBox.x, boundingBox.y, 0)
            );

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
