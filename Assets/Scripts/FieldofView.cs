using Unity.VisualScripting;
using UnityEngine;

public class FieldofView : MonoBehaviour
{
    private float viewRadius = 10f;
    private float viewAngle = 120f;
    public GameObject player;
    public LayerMask obstacleMask;
    public LayerMask targetMask;
    float threshold;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
                    // else
                    // {
                    //     // No obstacle in the way, the player is visible
                    //     Debug.Log("Player is in FOV and no obstacle detected");
                    // }
                }

            }
        }
        
        Debug.DrawRay(transform.position, transform.forward);
    }
}
