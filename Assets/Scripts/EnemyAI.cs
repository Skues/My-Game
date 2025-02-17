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
    float threshold;
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
                        ChasePlayer();

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
                    }
                    else if (isChasing && Vector3.Distance(transform.position, player.transform.position) > viewRadius)
                    {
                        StopChase();
                    }
                }
            }
        }
        
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    void ChasePlayer(){
        isChasing = true;
        agent.SetDestination(player.transform.position);
    }
    void StopChase(){
        isChasing = false;
        GotoNextPoint();
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
}
