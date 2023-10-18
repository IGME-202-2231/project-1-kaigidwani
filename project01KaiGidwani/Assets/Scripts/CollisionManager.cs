using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Kai Gidwani
// 10/17/23
// Manages the collisisons of all objects

public class CollisionManager : MonoBehaviour
{
    // Reference to the player
    [SerializeField] GameObject player;

    // References to all enemies
    [SerializeField] private List<GameObject> enemies;

    // Sprite for player bullet
    [SerializeField] private GameObject texturePlayerBullet;

    // Sprite for enemy bullet
    [SerializeField] private GameObject textureEnemyBullet;

    // List to hold all the enemy and player bullets
    private List<GameObject> playerBullets = new List<GameObject>();
    private List<GameObject> enemyBullets = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        // Reset all flags to false

        // Reset all player bullet flags to false
        foreach (GameObject sprite in playerBullets)
        {
            sprite.GetComponent<SpriteInfo>().IsColliding = false;
        }
        // Reset all enemy bullet flags to false
        foreach (GameObject sprite in enemyBullets)
        {
            sprite.GetComponent<SpriteInfo>().IsColliding = false;
        }
        // Reset all enemies flags to false
        foreach (GameObject sprite in enemies)
        {
            sprite.GetComponent<SpriteInfo>().IsColliding = false;
        }
        // Reset player flag to false
        player.GetComponent<SpriteInfo>().IsColliding = false;


        // Loop through and check all ENEMY BULLETS against the PLAYER
        foreach (GameObject enemyBullet in enemyBullets)
        {
            // Bool to hold if theyre colliding to set both objects
            // instead of running the check twice
            bool colliding = false;

            // Check if colliding
            colliding = aabbCheck(player, enemyBullet);

            // Set flags to true if it is, leave alone if not
            if (colliding)
            {
                // *** Do something with player getting hit here ***
                player.GetComponent<SpriteInfo>().IsColliding = true;
                enemyBullet.GetComponent<SpriteInfo>().IsColliding = true;

                // Remove the reference to the bullet
                enemyBullets.Remove(enemyBullet);
                // Get rid of the bullet from the scene
                Destroy(enemyBullet);

                // Do not remove the player haha
                // Reduce the player health here
            }
        }

        // Loop through and check all ENEMIES against the PLAYER BULLETS and
        // also check if an ENEMY is hitting the PLAYER
        foreach (GameObject enemy in enemies)
        {
            foreach (GameObject playerBullet in playerBullets)
            {
                // Bool to hold if theyre colliding to set both objects
                // instead of running the check twice
                bool pBulletColliding = false;

                // Check if colliding
                pBulletColliding = aabbCheck(enemy, playerBullet);

                // Set flags to true if it is, leave alone if not
                if (pBulletColliding)
                {
                    // *** Do something about enemy collision here ***
                    enemy.GetComponent<SpriteInfo>().IsColliding = true;
                    playerBullet.GetComponent<SpriteInfo>().IsColliding = true;


                    // Remove the reference to the bullet
                    playerBullets.Remove(playerBullet);
                    // Get rid of the bullet from the scene
                    Destroy(playerBullet);

                    // Remove the reference to the enemy
                    enemies.Remove(enemy);
                    // Get rid of the enemy from the scene
                    Destroy(enemy);
                }
            }

            // === Now check if that enemy is hitting the player themself ===

            // Bool to hold if theyre colliding to set both objects
            // instead of running the check twice
            bool colliding = false;

            // Check if colliding
            colliding = aabbCheck(enemy, player);

            // Set flags to true if it is, leave alone if not
            if (colliding)
            {
                // *** Do something with player getting hit here ***
                enemy.GetComponent<SpriteInfo>().IsColliding = true;
                player.GetComponent<SpriteInfo>().IsColliding = true;

                // Do not destroy the enemy or the player here
                // Maybe destroy enemy later, will playtest
                // Reduce player health
            }
        }
    }

    private bool aabbCheck(GameObject sA, GameObject sB)
    {
        return
            // minX of Box B < maxX of Box A
            sB.GetComponent<SpriteInfo>().RectMin.x < sA.GetComponent<SpriteInfo>().RectMax.x &&

            // maxX of Box B > minX of Box A
            sB.GetComponent<SpriteInfo>().RectMax.x > sA.GetComponent<SpriteInfo>().RectMin.x &&

            // maxY of Box B > minY of Box A
            sB.GetComponent<SpriteInfo>().RectMax.y > sA.GetComponent<SpriteInfo>().RectMin.y &&

            // minY of Box B < maxY of Box A
            sB.GetComponent<SpriteInfo>().RectMin.y < sA.GetComponent<SpriteInfo>().RectMax.y;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        // Create a player bullet
        playerBullets.Add(
            Instantiate(
                texturePlayerBullet,
                new Vector3(
                    player.transform.position.x,
                    player.transform.position.y + player.GetComponent<SpriteInfo>().BoxSize.y/2 + texturePlayerBullet.GetComponent<SpriteInfo>().BoxSize.y/2 + .1f,
                    0),
                Quaternion.identity)
                );
    }
}
