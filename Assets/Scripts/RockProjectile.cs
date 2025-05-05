using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
public class RockProjectile : MonoBehaviour
{
    bool hasHit = false;
    public float lifetime = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    void OnCollisionEnter(Collision collision)
    {
            // Prevent multiple hits
        if (hasHit) return;
        hasHit = true;

        if (collision.gameObject.tag == "Structure"){
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy"){
            EnemyAI enemy = collision.collider.GetComponent<EnemyAI>();
        
            if (enemy != null){
                enemy.TakeDamage(50);
                Destroy(gameObject);

                }
            
            }
        }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }  
}