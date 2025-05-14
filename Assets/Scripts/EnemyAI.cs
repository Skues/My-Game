using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public bool hasKey = false;
    public GameObject keyPrefab;
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    bool isChasing;
    public float viewRadius = 15f;
    public float viewAngle = 120f;
    public GameObject player;
    public LayerMask obstacleMask;
    public LayerMask targetMask;
    public GameObject enemy;
    public ExperienceBar experienceBar;
    public bool checkingSound = false;
    float threshold;
    public float hearingDistance = 5f;
    private bool isPatrolling = true;
    private int health = 100;

    public enum AIState { Idle, Suspicious, Alerted, Engaged }
    private float detectionLevel = 0f;
    private float maxDetection = 100f;
    public float detectionIncreaseRate = 20f;
    public float detectionDecreaseRate = 10f;
    private float detectionDelay = 2f;
    private AIState currentState = AIState.Idle;
    private float lastSeenTime = 0f;
    private bool canSeePlayer;
    private bool isAlerted = false;
    private bool isWaiting = false;
    public GameManager gameManager;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = transform.Find("character").GetComponent<Animator>();
        animator.SetBool("isWalking", true);
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        if (points != null && points.Length > 0){
                GotoNextPoint();
        }
        else{
                isPatrolling = false; // No patrol path, so this is a stationary enemy
        }
        threshold = Mathf.Cos(viewAngle * 0.5f * Mathf.Deg2Rad);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isWaiting) return; // Don't do anything while waiting
    
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

                        // print("PLAYER IN VIEW");
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
            this.GetComponent<NavMeshAgent>().speed = 5;
            if (!isChasing && !agent.pathPending && agent.remainingDistance < 0.5f)
                // print("STOPPPED CHASING");
                StopChase();
                // GotoNextPoint();
            break;

        case AIState.Suspicious:
            this.GetComponent<NavMeshAgent>().speed = 6f;
            agent.SetDestination(transform.position); // Pause and look around
            animator.SetBool("isWalking", false);
            break;

        case AIState.Alerted:
            animator.SetBool("isWalking", true);

            this.GetComponent<NavMeshAgent>().speed = 7f;
            agent.SetDestination(player.transform.position); // Move towards last seen position
            break;

        case AIState.Engaged:
            gameManager.PlayerCaught();
            this.GetComponent<NavMeshAgent>().speed = 4f;
            ChasePlayer();
            break;
        }
    // if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
    // {
    //     animator.SetBool("isWalking", false);
    // }
    // else
    // {
    //     animator.SetBool("isWalking", true);
    // }

    }

    void ChasePlayer(){
        isPatrolling = false;

        // Debug.Log("CHASING");
        isChasing = true;
        enemy.GetComponent<Renderer>().material.color = Color.red;
        agent.SetDestination(player.transform.position);

    }
    void StopChase()
{
    if (currentState != AIState.Idle) return;

    // Debug.Log("Stopping Chase");
    isChasing = false;
    enemy.GetComponent<Renderer>().material.color = Color.green;

    if (!agent.pathPending && agent.remainingDistance < 0.5f)
    {
        if (!isPatrolling)
        {
            // Just resume patrolling, don't jump to next point yet
            isPatrolling = true;
            GotoNextPoint();  // Set destination to the current point (no increment yet)
        }
        else
        {
            // Already patrolling normally, safe to move to next point
            StartCoroutine(WaitAndMove());
        }
    }
}
    void GotoNextPoint() {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

            // Set the agent to go to the currently selected destination.
        animator.SetBool("isWalking", true);
        
        agent.destination = points[destPoint].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
    }
    public void DetectSound(Vector3 soundPosition, float loudness)
    {
        float distance = Vector3.Distance(transform.position, soundPosition);
        if (distance <= (hearingDistance*loudness))
        {

            StartCoroutine(Investigate(soundPosition));
            checkingSound = false;
        }
    }
    IEnumerator Investigate(Vector3 soundPosition){
        checkingSound = true;
        agent.isStopped = true; // Stop movement
        animator.SetBool("isWalking", false);
        isPatrolling = false;
        yield return new WaitForSeconds(2f);
        agent.isStopped = false;
        animator.SetBool("isWalking", true);

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
        // Debug.Log(detectionLevel);
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
        Die();
    }
    IEnumerator WaitAndMove()
    {
    isWaiting = true;
    agent.isStopped = true; // Pause movement
    animator.SetBool("isWalking", false);


    float waitTime = Random.Range(1f, 2f);
    yield return new WaitForSeconds(waitTime);

    agent.isStopped = false; // Resume movement
    animator.SetBool("isWalking", true);

    destPoint = (destPoint + 1) % points.Length;

    GotoNextPoint();
    isWaiting = false;
    }

    public void TakeDamage(int amount){
        health -= amount;
        if (health <= 0){
            Die();
        }
    }
    void Die(){
        animator.SetBool("isDead", true);
        agent.isStopped = true;
        this.enabled = false;
        StartCoroutine(DestroyAfterAnimation());
        
    }
    IEnumerator DestroyAfterAnimation(){
        yield return new WaitForSeconds(2.1f);
        experienceBar.AddExperience(40);
        if (hasKey){
            Instantiate(keyPrefab, transform.position + Vector3.down *1.5f, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}

