using TMPro;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private float takedownRange = 4f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            StealthTakedown();
        }   
    }
    void StealthTakedown(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, takedownRange);
        foreach (Collider collider in hitColliders){
            EnemyAI enemy = collider.GetComponent<EnemyAI>();

            if (enemy != null && !enemy.getAlerted() && IsBehindEnemy(enemy)){
                enemy.PerformTakedown();
                break;
            }
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
