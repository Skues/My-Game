using UnityEngine;
using UnityEngine.UIElements;

public class RockProjectile : MonoBehaviour
{
    public float lifetime = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Structure"){
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy"){
            EnemyAI enemy = collision.collider.GetComponent<EnemyAI>();
            Rigidbody enemyRB = collision.collider.GetComponent<Rigidbody>();

            if (enemy != null){
                enemy.TakeDamage(50);
                if (enemyRB != null){
                    if (!enemyRB.CompareTag("KnockedBack")){
                        Vector3 forceDir = Camera.main.transform.forward;
                        forceDir.y = 0f;
                        forceDir = forceDir.normalized;
                        enemyRB.AddForce(forceDir*1f, ForceMode.Impulse);
                        collision.collider.tag = "KnockedBack";
                    }
                }
                    
            }
            Destroy(gameObject);
            print("damage enemy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
