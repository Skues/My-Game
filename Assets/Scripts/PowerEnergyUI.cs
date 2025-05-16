using UnityEngine;
using UnityEngine.UI;


public class PowerEnergyUI : MonoBehaviour
{
    public UnityEngine.UI.Image energyBar;
    public float maxEnergy = 100f;
    public float currentEnergy;
    private float rechargeRate = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(energyBar == null){
            GameObject energyBarGO = GameObject.Find("EnergyBarFill");
            energyBar = energyBarGO.GetComponent<UnityEngine.UI.Image>();
        }
    }
    void Start()
    {
        currentEnergy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnergy < maxEnergy){
            currentEnergy += rechargeRate *Time.deltaTime;
            currentEnergy = Mathf.Min(currentEnergy, maxEnergy);
        }
        energyBar.fillAmount = currentEnergy/maxEnergy;

    }
    public bool UsePower(int cost){
        if (currentEnergy >= cost){
            currentEnergy -= cost;
            return true;
        }
        return false;
    }
}
