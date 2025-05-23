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
    public int energyCost = 10;
    public PowerEnergyUI powerEnergyUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        powerEnergyUI = GameObject.Find("EnergyBarFill").GetComponent<PowerEnergyUI>();
        // characterController = GetComponent<CharacterController>();

        playerCamera = Camera.main;
        GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
        if (foundPlayer){
            player = foundPlayer;
            characterController = player.GetComponent<CharacterController>();
            // powerEnergyUI = player.GetComponent<PowerEnergyUI>();
        }
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
        else if (Input.GetKeyDown(KeyCode.Q)){
            teleportMarker.SetActive(false);
            abilityActive = false;
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
        if(powerEnergyUI.UsePower(energyCost)){

        
            characterController.enabled = false;
            player.transform.position = targetPosition + Vector3.up * 1f; // Lift to avoid clipping
            characterController.enabled = true;
            
            BlinkExecute();

            abilityActive = false;
            teleportMarker.SetActive(false);
        }
        else {
            abilityActive = false;
            teleportMarker.SetActive(false);
        }
    }
    void BlinkExecute(){
        Collider[] hits = Physics.OverlapSphere(player.transform.position, 4f);
        foreach (Collider hit in hits){
            if (hit.CompareTag("Boss")){
                BossHealth bossHealth = hit.GetComponent<BossHealth>();
                if (bossHealth){
                    bossHealth.BlinkKill();
                }
            } 
        }
    }
}
