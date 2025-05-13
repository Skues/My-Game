using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceBar : MonoBehaviour
{
    public static ExperienceBar Instance;

    public AnimationCurve experienceCurve;
    int currentLevel, totalExperience, previousLevelsExperience, nextLevelsExperience;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI experienceText;
    public Image experienceFill;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        UpdateLevel();
    }

    // Update is called once per frame
    void Update()
    {
    //     if (Input.GetMouseButtonDown(0)){
    //         AddExperience(5);
    //     }
    }
    public void AddExperience(int amount){
        totalExperience += amount;
        CheckForLevelUp();
        UpdateInterface();
    }
    void CheckForLevelUp(){
        if(totalExperience >= nextLevelsExperience){
            currentLevel += 1;
            UpdateLevel();
        }
    }
    void UpdateLevel(){
        previousLevelsExperience = (int)experienceCurve.Evaluate(currentLevel);
        nextLevelsExperience = (int)experienceCurve.Evaluate(currentLevel + 1);
        UpdateInterface();
    }
    void UpdateInterface(){
        int start = totalExperience- previousLevelsExperience;
        int end = nextLevelsExperience - previousLevelsExperience;

        levelText.text = currentLevel.ToString();
        experienceText.text = start + " exp / " + end + " exp";
        experienceFill.fillAmount = (float)start / (float)end;
    }
}
