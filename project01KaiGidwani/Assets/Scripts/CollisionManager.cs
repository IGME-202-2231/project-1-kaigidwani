using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionManager : MonoBehaviour
{
    // List to hold all the sprites
    [SerializeField] List<SpriteInfo> sprites = new List<SpriteInfo>();

    // Update is called once per frame
    void Update()
    {
        // Reset all flags to false
        foreach (SpriteInfo sprite in sprites)
        {
            sprite.IsColliding = false;
        }

        // Loop through and check everything against each other
        foreach (SpriteInfo secondSprite in sprites)
        {
            // Makes sure we aren't comparing the sprite against itself
            if (sprites[0] == secondSprite)
            {
                continue;
            }

            // Bool to hold if theyre colliding to set both objects
            // instead of running the check twice
            bool colliding = false;

            // Check if colliding
            colliding = aabbCheck(sprites[0], secondSprite);

            // Set flags to true if it is, leave alone if not
            if (colliding)
            {
                sprites[0].IsColliding = true;
                secondSprite.IsColliding = true;
            }
        }
    }

    private bool aabbCheck(SpriteInfo sA, SpriteInfo sB)
    {
        return
            // minX of Box B < maxX of Box A
            sB.RectMin.x < sA.RectMax.x &&

            // maxX of Box B > minX of Box A
            sB.RectMax.x > sA.RectMin.x &&

            // maxY of Box B > minY of Box A
            sB.RectMax.y > sA.RectMin.y &&

            // minY of Box B < maxY of Box A
            sB.RectMin.y < sA.RectMax.y;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        
    }
}
