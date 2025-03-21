using UnityEngine;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;

public class DetectionMeter : MonoBehaviour
{
    public Transform enemy;
    public UnityEngine.UI.Image meterBackground;
    public UnityEngine.UI.Image detectionBar;
    private EnemyAI enemyAI;

    // Update is called once per frame
    void Start()
    {
        enemyAI = enemy.GetComponent<EnemyAI>();   
    }
    void Update()
    {
        float currentDetection = enemyAI.getCurrentDetection();
        detectionBar.fillAmount = currentDetection/100;
    }
}
