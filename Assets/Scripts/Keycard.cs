using UnityEngine;

public class Keycard : MonoBehaviour
{
    public static bool hasKeycard = false;
    public IDoor door1;
    public IDoor door2;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            hasKeycard = true;
            Destroy(gameObject);
        }
    }
}
