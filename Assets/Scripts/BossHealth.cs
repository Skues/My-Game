using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHits = 5;
    private int currentHits;
    private BossAI bossAI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHits = 0;
        bossAI = GetComponent<BossAI>();
    }
    public void TakeHit(){
        currentHits ++;
        if (currentHits >= maxHits){
            bossAI.Die();
        }
    }
    public void BlinkKill(){
        if(bossAI.GetVulnerable()){
            bossAI.Die();
        }

    }
    void Die(){
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
