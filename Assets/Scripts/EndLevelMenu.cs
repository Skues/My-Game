using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelMenu : MonoBehaviour
{
    public TextMeshProUGUI enemiesKilledText;
    public TextMeshProUGUI timeTakenText;
    public TextMeshProUGUI timesSpottedText;
    public TextMeshProUGUI secretsFoundText;
    public TextMeshProUGUI killsXPText;
    public TextMeshProUGUI spottedXPText;
    public TextMeshProUGUI secretsXPText;
    public TextMeshProUGUI totalXPText;


    void Start()
    {
        var stats = LevelStats.Instance;
        enemiesKilledText.text = $"Enemies Killed: {stats.enemiesKilled}";
        timeTakenText.text = $"Time: {FormatTime(stats.timeTaken)}";
        timesSpottedText.text = $"Times Spotted: {stats.timesSpotted}";
        secretsFoundText.text = $"Secrets Found: {stats.secretsFound}";
        int timexp;
        if (stats.timesSpotted == 0){
            timexp = -50;
        }
        else{
            
            timexp = stats.timesSpotted*5;
        }
        int killsXP = stats.enemiesKilled * 10;
        int secretsXP = stats.secretsFound * 20;
        int xpGained = stats.enemiesKilled * 10 - timexp + stats.secretsFound * 20;
        xpGained = Mathf.Max(0, xpGained);

        if (killsXPText != null) killsXPText.text = $"+ Kills: +{killsXP} XP";
        if (spottedXPText != null) spottedXPText.text = $"- Spotted: {timexp} XP ";
        if (secretsXPText != null) secretsXPText.text = $"+ Secrets: +{secretsXP} XP";
        if (totalXPText != null) totalXPText.text = $"Total XP -> [ +{xpGained} ]";
        ExperienceBar.Instance.AddExperience(xpGained);

    }
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return $"{minutes}:{seconds:00}";
    }
    public void LoadNextLevel(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Level2");
    }
    public void ExitGame(){
        SceneManager.LoadScene("MainMenu");
    }
}