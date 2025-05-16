using UnityEngine;

public class EarthPower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform startPoint;
    private float shootForce = 30f;
    private float cooldown = 1f;
    private float nextShot = 0f;
    private int rockCost = 10;
    public GameObject powerEnergyUI;
    private PowerEnergyUI powerEnergy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Awake()
    {
        if(powerEnergyUI == null){
            powerEnergyUI = GameObject.Find("EnergyBarFill");
            if (powerEnergyUI){
                powerEnergy = powerEnergyUI.GetComponent<PowerEnergyUI>();
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextShot && powerEnergy.UsePower(rockCost)){
            ShootProjectile();
            nextShot = Time.time + cooldown;
        }
    }
    void ShootProjectile(){
        Vector3 targetDirection;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, 100f)){
            targetDirection = (hit.point - startPoint.position).normalized;
        }
        else{
            targetDirection = ray.direction;
        }
        Transform cam = Camera.main.transform;
        Transform spawnPoint = startPoint != null ? startPoint : transform;
        GameObject proj = Instantiate(projectilePrefab, spawnPoint.position + spawnPoint.forward, Quaternion.identity);
        Rigidbody rb = proj.GetComponent<Rigidbody>();

        if (rb != null){
            rb.linearVelocity = targetDirection * shootForce;
        }
    }
}
