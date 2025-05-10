using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public string newObjective;
    public ObjectiveManager objectiveManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectiveManager.SetObjective(newObjective);
            Destroy(gameObject); // Only trigger once
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
