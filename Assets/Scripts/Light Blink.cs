using UnityEngine;

public class LightBlink : MonoBehaviour
{

    public float teleportDistance = 20f;
    public LayerMask groundMask;
    public GameObject teleportMarkerPrefab;
    private Camera playerCamera;
    private GameObject teleportMarker;
    private bool abilityActive = false;
    private Vector3 targetPosition;
    public GameObject player;
    public CharacterController characterController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        // characterController = GetComponent<CharacterController>();

        playerCamera = Camera.main;
        if (teleportMarkerPrefab != null){
            teleportMarker = Instantiate(teleportMarkerPrefab);
            teleportMarker.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            abilityActive = true;
            teleportMarker.SetActive(true);
        }
        if (abilityActive){
            UpdateTargetLocation();
            if (Input.GetKeyDown(KeyCode.Mouse1)){
                Teleport();
            }
        }

    }

    void UpdateTargetLocation(){
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, teleportDistance, groundMask))
        {
            targetPosition = hit.point;
            teleportMarker.transform.position = targetPosition + Vector3.up * 0.1f; // Slight offset
        }
        else
        {
            teleportMarker.transform.position = Vector3.zero;
        }

    }
    void Teleport()
    {
        characterController.enabled = false;
        player.transform.position = targetPosition + Vector3.up * 1f; // Lift to avoid clipping
        characterController.enabled = true;

        abilityActive = false;
        teleportMarker.SetActive(false);
    }

}
