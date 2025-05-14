using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
public class RockProjectile : MonoBehaviour
{
    public SoundDetection soundEmitter;
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
            soundEmitter.CreateSound(transform.position, 5f);
            Destroy(gameObject);    
        }
        else if (collision.gameObject.tag == "Enemy"){
            EnemyAI enemy = collision.collider.GetComponent<EnemyAI>();
        
            if (enemy != null){
                enemy.TakeDamage(50);
                Destroy(gameObject);

                }
            
            }
        else if(collision.gameObject.tag == "Boss"){
            BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();
            if(bossHealth){
                bossHealth.TakeHit();

            }
        }
        Destroy(gameObject);
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }  
}