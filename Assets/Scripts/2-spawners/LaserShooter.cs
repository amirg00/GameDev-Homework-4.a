using System.Collections;
using UnityEngine;

/**
 * This component spawns the given laser-prefab whenever the player clicks a given key.
 * It also updates the "scoreText" field of the new laser.
 */
public class LaserShooter: ClickSpawner {
    [SerializeField] NumberField scoreField;
    [Tooltip("Player's shooting rate")]
    [SerializeField] private float fireRate = 0.5f;
    private bool canShoot = true;

    protected override GameObject spawnObject() {
        // Player cannot shoot and the object will not be spawned.
        if (!canShoot)
        {
            return null;
        }
        
        GameObject newObject = base.spawnObject();  // base = super

        // Modify the text field of the new object.
        ScoreAdder newObjectScoreAdder = newObject.GetComponent<ScoreAdder>();
        if (newObjectScoreAdder)
            newObjectScoreAdder.SetScoreField(scoreField);
        
        
        canShoot = false;
        this.StartCoroutine(ShootingCooldown(fireRate));
        return newObject;
    }
    
    // Function delays shooting by a given amount of time.
    private IEnumerator ShootingCooldown(float cooldownTime)
    {
        // Wait for the fireRate before setting canShoot back to true.
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }
    
    
    
}
