using UnityEngine;
using TMPro;
public class ObjectiveManager : MonoBehaviour
{

    public TextMeshProUGUI objectiveText;
    public void SetObjective(string newObjective)
    {
        objectiveText.text = newObjective;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectiveText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
