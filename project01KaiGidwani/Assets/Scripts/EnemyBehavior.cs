using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // Enum for enemy type
    enum EnemyType
    {
        SmallEnemy,
        LargeEnemy
    }

    // Fields

    // Type of enemy
    [SerializeField] EnemyType enemyType;

    // How often in the random interval to shoot.
    // Higher number means shooting more often.
    [SerializeField, Range(1, 10)] int shootingAmount;

    // How often in time to shoot. The cooldown between shots.
    // Higher number means shooting less often.
    [SerializeField, Range(1, 5)] float shootingInterval;

    // Time util allowed to try to shoot
    private float nextShot;

    // Bullet to shoot
    [SerializeField] GameObject bulletType;


    // Start is called before the first frame update
    private void Start()
    {
        // Start game with half enemy shooting cooldown
        nextShot = shootingInterval/2;
    }

    // Shoot assigned projectile at assigned interval
    public GameObject Fire()
    {
        // If the random number is within the threshold set by shootingAmount
        if (Time.time > nextShot)
        {
            if (Random.Range(1, 1000) <= shootingAmount)
            {
                // Reset shooting timer
                nextShot = Time.time + shootingInterval;

                // Create a new bullet object
                return Instantiate(
                        bulletType,
                        new Vector3(
                            transform.position.x,
                            transform.position.y - this.GetComponent<SpriteInfo>().BoxSize.y / 2 - bulletType.GetComponent<SpriteInfo>().BoxSize.y / 2 - .1f,
                            0),
                        Quaternion.identity);
            }
        }

        // If no bullet gets spawned, return null
        return null;
    }
}
