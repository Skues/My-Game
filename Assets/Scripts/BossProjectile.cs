using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public int damage = 50;
    public float lifeTime = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth){
                playerHealth.TakeDamage(damage);
                Destroy(gameObject);
            }
            
        }
    }

}
