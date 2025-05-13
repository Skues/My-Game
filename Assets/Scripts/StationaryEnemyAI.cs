using UnityEngine;
using UnityEngine.AI;

public class StationaryEnemyAI : MonoBehaviour
{
    public bool hasKey = false;
    public GameObject keyPrefab;
    private NavMeshAgent agent;
    public float viewRadius = 15f;
    public float viewAngle = 120f;
    public GameObject player;
    public LayerMask obstacleMask;
    public LayerMask targetMask;
    public GameObject enemy;
    public ExperienceBar experienceBar;
    public float hearingDistance = 5f;
    private int health = 100;

    public enum AIState { Idle, Suspicious, Alerted, Engaged }
    private AIState currentState = AIState.Idle;
    private float detectionLevel = 0f;
    private float maxDetection = 100f;
    public float detectionIncreaseRate = 20f;
    public float detectionDecreaseRate = 10f;
    private float detectionDelay = 2f;
    private float lastSeenTime = 0f;
    private float threshold;
    private bool canSeePlayer;
    public GameManager gameManager;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        threshold = Mathf.Cos(viewAngle * 0.5f * Mathf.Deg2Rad);
    }

    void Update()
    {
        canSeePlayer = false;

        Collider[] targetsInView = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        if (targetsInView.Length > 0)
        {
            Transform target = targetsInView[0].transform;
            Vector3 direction = (target.position - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, direction);

            if (dot > threshold)
            {
                float dist = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, direction, out RaycastHit hit, dist, obstacleMask))
                {
                    detectionLevel += detectionIncreaseRate * Time.deltaTime;
                    lastSeenTime = Time.time;
                    canSeePlayer = true;
                }
            }
        }

        if (!canSeePlayer && Time.time > lastSeenTime + detectionDelay)
        {
            detectionLevel -= detectionDecreaseRate * Time.deltaTime;
        }

        detectionLevel = Mathf.Clamp(detectionLevel, 0, maxDetection);
        UpdateAIState();

        switch (currentState)
        {
            case AIState.Idle:
                agent.isStopped = true;
                break;

            case AIState.Suspicious:
                agent.isStopped = true;
                break;

            case AIState.Alerted:
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
                break;

            case AIState.Engaged:
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
                gameManager.PlayerCaught();
                break;
        }
    }

    void UpdateAIState()
    {
        if (detectionLevel <= 25f)
            currentState = AIState.Idle;
        else if (detectionLevel <= 50f)
            currentState = AIState.Suspicious;
        else if (detectionLevel < 90f)
            currentState = AIState.Alerted;
        else
            currentState = AIState.Engaged;
    }

    public void DetectSound(Vector3 soundPos, float loudness)
    {
        if (Vector3.Distance(transform.position, soundPos) <= hearingDistance * loudness)
        {
            agent.SetDestination(soundPos);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        experienceBar.AddExperience(40);
        if (hasKey)
        {
            Instantiate(keyPrefab, transform.position + Vector3.down * 1.5f, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public void PerformTakedown()
    {
        Die();
    }

    public float getCurrentDetection(){
        return detectionLevel;
    }    
    public bool IsAlerted() => currentState != AIState.Idle;
}

