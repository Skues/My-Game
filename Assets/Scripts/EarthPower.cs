using UnityEngine;

public class EarthPower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform startPoint;
    private float shootForce = 30f;
    private float cooldown = 2f;
    private float nextShot = 0f;
    private int rockCost = 20;
    public PowerEnergyUI powerEnergyUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextShot && powerEnergyUI.UsePower(rockCost)){
            ShootProjectile();
            nextShot = Time.time + cooldown;
        }
    }
    void ShootProjectile(){
        Transform cam = Camera.main.transform;
        Transform spawnPoint = startPoint != null ? startPoint : transform;
        GameObject proj = Instantiate(projectilePrefab, spawnPoint.position + spawnPoint.forward, Quaternion.identity);
        Rigidbody rb = proj.GetComponent<Rigidbody>();

        if (rb != null){
            rb.linearVelocity = cam.forward *shootForce;
        }
    }
}
