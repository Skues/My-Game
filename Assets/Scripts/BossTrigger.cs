using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public Transform bossFightSpawn;
    public GameObject boss;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller){
                controller.enabled = false;
                other.transform.position = bossFightSpawn.position;
                controller.enabled = true;

            }
        
            boss.SetActive(true);
            
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
