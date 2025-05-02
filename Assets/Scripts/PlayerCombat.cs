using TMPro;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private float takedownRange = 6f;
    private EnemyAI enemy;
    public GameObject takedownUI;

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, takedownRange);
        enemy = null;
        foreach (Collider collider in hitColliders){
            EnemyAI potentialEnemy = collider.GetComponent<EnemyAI>();
            if (potentialEnemy != null && IsBehindEnemy(potentialEnemy))
            {
                enemy = potentialEnemy;
                break; // Stop at the first valid enemy behind
            }
        }
        if (enemy != null){
            takedownUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)){
                StealthTakedown();
            }   
        } else{
            takedownUI.SetActive(false);
        }
        
        
    }
    void StealthTakedown(){
        if (enemy != null && !enemy.getAlerted() && IsBehindEnemy(enemy)){
            enemy.PerformTakedown();
        }
    }
    
    bool IsBehindEnemy(EnemyAI enemy){
        Vector3 toPlayer = transform.position - enemy.transform.position;
        float dot = Vector3.Dot(enemy.transform.forward, toPlayer.normalized);
        if (dot < -0.7f){
            print("KILL");
            return true;
        }
        else return false;
    }
}
