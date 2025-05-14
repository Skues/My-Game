using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Transform player;
    public GameObject projectilePrefab;
    public Transform firePoint;

    public float burstInterval = 5f;
    public int shots = 3;
    public float shotCooldown = 0.5f;
    public float projectileSpeed = 15f;
    public float vulnerableInterval = 15f;
    public float vulnerableDuration = 5f;
    
    [Header("Debug")]
    public bool isVulnerable = false;
    public bool isShooting = false;
    
    private float burstTimer;
    private float vulnerableTimer;
    private int shotsRemaining;
    private float shotWait;

    void Start()
    {
        burstTimer = burstInterval;
        vulnerableTimer = vulnerableInterval;
        isVulnerable = false;
        
        // Auto-find player if not assigned
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                Debug.Log("Player found automatically");
            }
            else
            {
                Debug.LogError("No player found! Please assign the player in the inspector or tag your player GameObject with 'Player'");
            }
        }
        
        // Check other required components
        if (projectilePrefab == null)
        {
            Debug.LogError("No projectile prefab assigned! Please assign it in the inspector");
        }
        
        if (firePoint == null)
        {
            Debug.LogWarning("No fire point assigned! Using this transform as fire point");
            firePoint = transform;
        }
    }

    void Update()
    {
        // Early return if essential components are missing
        if (player == null || projectilePrefab == null)
        {
            return;
        }
        
        FacePlayer();
        HandleShooting();
        HandleVulnerability();
    }
    
    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Keep the boss level
        
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }
    }
    
    void HandleShooting()
    {
        if (isVulnerable) return; // don't attack when vulnerable

        if (!isShooting)
        {
            burstTimer -= Time.deltaTime;
            if (burstTimer <= 0f)
            {
                isShooting = true;
                shotsRemaining = shots;
                shotWait = 0f;
                burstTimer = burstInterval;
                Debug.Log("Boss starting attack burst");
            }
        }
        else
        {
            shotWait -= Time.deltaTime;
            if (shotWait <= 0f && shotsRemaining > 0)
            {
                ShootProjectile();
                shotsRemaining--;
                shotWait = shotCooldown;
            }

            if (shotsRemaining <= 0)
            {
                isShooting = false;
                Debug.Log("Boss finished attack burst");
            }
        }
    }
    
    void ShootProjectile()
    {
        Vector3 spawnPosition = firePoint.position + firePoint.forward * 1f;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        Vector3 direction = (player.position - spawnPosition).normalized;
        
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
            Debug.Log("Boss fired projectile at player");
            Debug.Log(projectile.transform.position);
        }
        else
        {
            Debug.LogError("Projectile prefab has no Rigidbody component!");
        }
        
        // Destroy projectile after 5 seconds to prevent memory leaks
    }
    
    void HandleVulnerability()
    {
        vulnerableTimer -= Time.deltaTime;

        if (!isVulnerable && vulnerableTimer <= 0f)
        {
            isVulnerable = true;
            Vulnerable();
            Invoke(nameof(ResetVulnerability), vulnerableDuration);
        }
    }
    
    void Vulnerable()
    {
        Debug.Log("BOSS IS VULNERABLE");
        // Add visual/audio cues here
    }
    
    void ResetVulnerability()
    {
        isVulnerable = false;
        vulnerableTimer = vulnerableInterval;
        Debug.Log("BOSS IS NOT VULNERABLE");
    }
    
    public bool GetVulnerable()
    {
        return isVulnerable;
    }

}
