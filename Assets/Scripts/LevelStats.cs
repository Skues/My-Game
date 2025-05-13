using UnityEngine;

public class LevelStats : MonoBehaviour
{
    public static LevelStats Instance;
    public int enemiesKilled;
    public float timeTaken;
    public int timesSpotted;
    public int secretsFound;
    private float timer;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timeTaken = timer;
    }
    public void EnemyKilled() => enemiesKilled++;
    public void PlayerSpotted() => timesSpotted++;
    public void SecretFound() => secretsFound++;

}
