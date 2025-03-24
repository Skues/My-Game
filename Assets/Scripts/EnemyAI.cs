using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    bool isChasing;
    private float viewRadius = 15f;
    private float viewAngle = 120f;
    public GameObject player;
    public LayerMask obstacleMask;
    public LayerMask targetMask;
    public GameObject enemy;
    float threshold;
    public float hearingDistance = 5f;

    public enum AIState { Idle, Suspicious, Alerted, Engaged }
    private float detectionLevel = 0f;
    private float maxDetection = 100f;
    private float detectionIncreaseRate = 20f;
    private float detectionDecreaseRate = 10f;
    private float detectionDelay = 2f;
    private AIState currentState = AIState.Idle;
    private float lastSeenTime = 0f;
    private bool canSeePlayer;
    private bool isAlerted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GotoNextPoint();
        threshold = Mathf.Cos(viewAngle * 0.5f * Mathf.Deg2Rad);
    }

    // Update is called once per frame
    void Update()
    {
        canSeePlayer = false;

        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        if (targetInViewRadius.Length > 0)
        {
            Transform target = targetInViewRadius[0].transform;
            Vector3 direction = (target.position - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, direction);
            if (dot > threshold)
            {
                if (Vector3.Angle(transform.forward, direction) < viewAngle/2){
                    float distToTarget = Vector3.Distance(transform.position, target.position);
                    RaycastHit hit;
                    if (!Physics.Raycast(transform.position, direction, out hit, distToTarget, obstacleMask))
                    {
                        detectionLevel += detectionIncreaseRate * Time.deltaTime;
                        lastSeenTime = Time.time;
                        canSeePlayer = true;
                        // ChasePlayer();

                        print("PLAYER IN VIEW");
                        // if (hit.collider.gameObject == player)
                        // {
                        //     Debug.Log("Player is visible!");
                        //     // Enemy can now react (e.g., chase or alert others)
                        // }
                        // else
                        // {
                        //     Debug.Log("Player is in FOV but blocked by " + hit.collider.gameObject.name);
                        // }
                        //Debug.Log("Checking if chase should stop: isChasing=" + isChasing + ", distance=" + Vector3.Distance(transform.position, player.transform.position));

                    }
        
                }
            }
        }
    if (!canSeePlayer && Time.time > lastSeenTime + detectionDelay)
    {
        detectionLevel -= detectionDecreaseRate * Time.deltaTime; // Gradually decrease suspicion
    }
        
        
    detectionLevel = Mathf.Clamp(detectionLevel, 0, maxDetection);
    UpdateAIState();


    switch (currentState)
        {
        case AIState.Idle:
            this.GetComponent<NavMeshAgent>().speed = 3;
            if (!isChasing && !agent.pathPending && agent.remainingDistance < 0.5f)
                print("STOPPPED CHASING");
                StopChase();
                // GotoNextPoint();
            break;

        case AIState.Suspicious:
            this.GetComponent<NavMeshAgent>().speed = 3f;
            agent.SetDestination(transform.position); // Pause and look around
            break;

        case AIState.Alerted:
            this.GetComponent<NavMeshAgent>().speed = 3.5f;
            agent.SetDestination(player.transform.position); // Move towards last seen position
            break;

        case AIState.Engaged:
            this.GetComponent<NavMeshAgent>().speed = 4f;
            ChasePlayer();
            break;
        }
    }

    void ChasePlayer(){
        Debug.Log("CHASING");
        isChasing = true;
        enemy.GetComponent<Renderer>().material.color = Color.red;
        agent.SetDestination(player.transform.position);

    }
    void StopChase(){
        if (currentState != AIState.Idle) return; // Prevent unnecessary calls

        Debug.Log("Stopping Chase");
        isChasing = false;
        enemy.GetComponent<Renderer>().material.color = Color.green;

        if (!agent.pathPending && agent.remainingDistance < 0.5f) {
            GotoNextPoint();
        }
    }
    void GotoNextPoint() {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

            // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }
    public void DetectSound(Vector3 soundPosition, float loudness)
    {
        float distance = Vector3.Distance(transform.position, soundPosition);
        if (distance <= (hearingDistance*loudness))
        {

            Investigate(soundPosition);
        }
    }
    void Investigate(Vector3 soundPosition){
        this.GetComponent<NavMeshAgent>().speed = 1.5f;
        agent.SetDestination(soundPosition);
        Debug.Log("Sound detected");
    }
    void UpdateDetection(bool canSeePlayer)
    {
        if (canSeePlayer)
        {
            detectionLevel += detectionIncreaseRate * Time.deltaTime;
            lastSeenTime = Time.time;
        }
        else if (Time.time > lastSeenTime + detectionDelay)
        {
            detectionLevel -= detectionDecreaseRate * Time.deltaTime;
        }

        detectionLevel = Mathf.Clamp(detectionLevel, 0, maxDetection);
        UpdateAIState();
    }
    void UpdateAIState() {
        Debug.Log(detectionLevel);
        if (detectionLevel <= 25f)
            {currentState = AIState.Idle;}
        else if (detectionLevel > 25f && detectionLevel <= 50f)
            {currentState = AIState.Suspicious;}
        else if (detectionLevel > 50f && detectionLevel < 90f)
            {currentState = AIState.Alerted;}
        else if (detectionLevel >= 90f)
            {currentState = AIState.Engaged;}

    }
    public float getCurrentDetection(){
        return detectionLevel;
    }
    public bool getAlerted(){
        return isAlerted;
    }


    bool CanSeePlayer()
{
    RaycastHit hit;
    Vector3 direction = (player.transform.position - transform.position).normalized;

    if (Physics.Raycast(transform.position, direction, out hit, viewRadius))
    {
        return hit.collider.CompareTag("Player"); // Ensure your player has the tag "Player"
    }

    return false;
}
    public void PerformTakedown()
    {
        isAlerted = false;
        Destroy(gameObject, 2f);
    }
}