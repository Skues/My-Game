using UnityEngine;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;

public class StationaryDetectionMeter : MonoBehaviour
{
    public GameObject enemy;
    public GameObject meterBackground;
    public UnityEngine.UI.Image detectionBar;
    private StationaryEnemyAI enemyAI;

    // Update is called once per frame
    void Start()
    {
        enemyAI = enemy.GetComponent<StationaryEnemyAI>();   
    }
    void Update()
    {
        
        float currentDetection = enemyAI.getCurrentDetection();
        detectionBar.fillAmount = currentDetection/100;

        if (currentDetection > 0){
            meterBackground.SetActive(true);
        }
        else{
            meterBackground.SetActive(false);
        }
    }
}
