using UnityEngine;

public class EarthPower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform startPoint;
    public float shootForce = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            ShootProjectile();
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
