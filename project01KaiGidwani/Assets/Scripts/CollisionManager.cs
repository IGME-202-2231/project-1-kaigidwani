using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Kai Gidwani
// 10/17/23
// Manages the collisisons of all objects

public class CollisionManager : MonoBehaviour
{
    // Reference to the player
    [SerializeField] GameObject player;

    // References to all enemies
    [SerializeField] private List<GameObject> enemies;

    // Prefab for player bullet
    [SerializeField] private GameObject texturePlayerBullet;

    // Prefab for small enemy bullet
    [SerializeField] private GameObject smallEnemyBullet;

    // Prefab for big enemy bullet
    [SerializeField] private GameObject bigEnemyBullet;

    // Start player health at 3
    private int playerHealth = 3;

    // Start player score at 0
    private int playerScore = 0;

    // List to hold all the enemy and player bullets
    [SerializeField] private List<GameObject> playerBullets = new List<GameObject>();
    [SerializeField] private List<GameObject> enemyBullets = new List<GameObject>();

    // Textmesh to display the score
    [SerializeField] public Text scoreTextMesh;
    // Textmesh to display the lives
    [SerializeField] public Text healthTextMesh;

    // Bool if game is paused
    private bool gamePaused = false;

    // Update is called once per frame
    void Update()
    {
        // === Reset all collision flags to false ===

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


        // === Check the collisions of objects against other objects ===

        // Create lists of items to remove if they are colliding
        List<GameObject> playerBulletsToRemove = new List<GameObject>();
        List<GameObject> enemyBulletsToRemove = new List<GameObject>();
        List<GameObject> enemiesToRemove = new List<GameObject>();

        // Loop through and check all ENEMY BULLETS against the PLAYER
        foreach (GameObject enemyBullet in enemyBullets)
        {
            foreach (GameObject playerBullet in playerBullets)
            {
                // === Check if player and enemy bullets are colliding ===

                // Bool to hold if theyre colliding to set both objects
                // instead of running the check twice
                bool bulletColliding = false;

                // Check if colliding
                bulletColliding = aabbCheck(enemyBullet, playerBullet);

                // Set flags to true if it is, leave alone if not
                if (bulletColliding)
                {
                    playerBullet.GetComponent<SpriteInfo>().IsColliding = true;
                    enemyBullet.GetComponent<SpriteInfo>().IsColliding = true;

                    // Add the reference of the enemy bullet to the list to be removed
                    enemyBulletsToRemove.Add(enemyBullet);

                    // Add the reference of the player bullet to the list to be removed
                    playerBulletsToRemove.Add(playerBullet);
                }
            }


            // === Now check if the player is colliding with an enemy bullet ===

            // Bool to hold if theyre colliding to set both objects
            // instead of running the check twice
            bool playerColliding = false;

            // Check if colliding
            playerColliding = aabbCheck(player, enemyBullet);

            // Set flags to true if it is, leave alone if not
            if (playerColliding)
            {
                player.GetComponent<SpriteInfo>().IsColliding = true;
                enemyBullet.GetComponent<SpriteInfo>().IsColliding = true;

                // Add the reference of the enemy bullet to the list to be removed
                enemyBulletsToRemove.Add(enemyBullet);

                // Reduce the player health for getting hit
                playerHealth--;
            }
        }

        // Loop through and check all ENEMIES against the PLAYER BULLETS and
        // also check if an ENEMY is hitting the PLAYER
        foreach (GameObject enemy in enemies)
        {
            foreach (GameObject playerBullet in playerBullets)
            {
                // === Check if player bullets are hitting enemies ===

                // Bool to hold if theyre colliding to set both objects
                // instead of running the check twice
                bool pBulletColliding = false;

                // Check if colliding
                pBulletColliding = aabbCheck(enemy, playerBullet);

                // Set flags to true if it is, leave alone if not
                if (pBulletColliding)
                {
                    enemy.GetComponent<SpriteInfo>().IsColliding = true;
                    playerBullet.GetComponent<SpriteInfo>().IsColliding = true;

                    // Add to player score
                    playerScore += 300;

                    // Add the reference of the player bullet to the list to be removed
                    playerBulletsToRemove.Add(playerBullet);

                    // Add the reference of the enemy to the list to be removed
                    enemiesToRemove.Add(enemy);
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
                enemy.GetComponent<SpriteInfo>().IsColliding = true;
                player.GetComponent<SpriteInfo>().IsColliding = true;

                // Add to player score
                playerScore += 100;

                // Add the reference of the enemy to the list to be removed
                enemiesToRemove.Add(enemy);

                // Reduce player health for getting hit
                playerHealth--;
            }
        }


        // === Remove items that need to be removed ===

        // Remove the enemy bullets
        foreach (GameObject enemyBullet in enemyBulletsToRemove)
        {
            // Remove the reference to the bullet
            enemyBullets.Remove(enemyBullet);
            // Get rid of the bullet from the scene
            Destroy(enemyBullet);
        }

        // Remove the player bullets
        foreach (GameObject playerBullet in playerBulletsToRemove)
        {
            // Remove the reference to the bullet
            playerBullets.Remove(playerBullet);
            // Get rid of the bullet from the scene
            Destroy(playerBullet);
        }

        // Remove the enemy bullets
        foreach (GameObject enemy in enemiesToRemove)
        {
            // Remove the reference to the enemy
            enemies.Remove(enemy);
            // Get rid of the enemy from the scene
            Destroy(enemy);
        }


        // === Enemy Firing ===

        // If game is unpaused
        if (!gamePaused)
        {
            // Loop through every enemy
            foreach (GameObject enemy in enemies)
            {
                // Try to shoot a bullet and save it in bullet
                GameObject bullet = enemy.GetComponent<EnemyBehavior>().Fire();

                // If the shooting was successful
                if (bullet != null)
                {
                    // Add the bullet to the list
                    enemyBullets.Add(bullet);
                }
            }
        }


        // === Update UI ===

        // Update the score and health
        scoreTextMesh.text = "Score: " + playerScore;
        healthTextMesh.text = "Health: " + playerHealth;


        // === Check game ending conditions ===

        // Less than or equal to 0 because it is technically possible although
        // highly unlikely to get less than 0 health
        // if you get hit by an enemy and a bullet on the same frame
        if (playerHealth <= 0) 
        {
            // If you lose, pause all enemies
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<MovementController>().Speed = 0;
            }

            // Pause and hide the player
            gamePaused = true;
            player.GetComponent<MovementController>().Speed = 0;
            player.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
        // If there are no more enemies, you win and pause movement and shooting
        else if (enemies.Count == 0)
        {
            // If you win, reduce player speed to 0
            player.GetComponent<MovementController>().Speed = 0;
            // Make it so player can't shoot
            gamePaused = true;
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
        // Only shoots once per button press and if the player is allowed to be shooting
        if (context.phase == InputActionPhase.Performed && !gamePaused)
        {
            // Create a player bullet
            playerBullets.Add(
                Instantiate(
                    texturePlayerBullet,
                    new Vector3(
                        player.transform.position.x,
                        player.transform.position.y + player.GetComponent<SpriteInfo>().BoxSize.y / 2 + texturePlayerBullet.GetComponent<SpriteInfo>().BoxSize.y / 2 + .1f,
                        0),
                    Quaternion.identity)
                    );
        }
    }
}
