using UnityEngine;

public class DoorInteractor : MonoBehaviour
{
    public DoorController door1;
    public DoorController door2;
    private bool playerInZone = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.F)){
            if(Keycard.hasKeycard){
                door1.UnlockDoor();
                door2.UnlockDoor();
                door1.ToggleDoor();
                door2.ToggleDoor();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            playerInZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")){
            playerInZone = false;
        }
    }
}
