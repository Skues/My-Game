using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float range = 3f;
    public LayerMask interactableLayer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            print("F KEY PRESSED");
            Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, range, interactableLayer))
            {
                DoorController door = hit.collider.GetComponent<DoorController>();
                if (door != null)
                {
                    door.ToggleDoor();
                }
            }
        }
    }
}
