using UnityEngine;

public class SceneMang : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject UI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // GameObject spawnPoint = GameObject.FindWithTag("PlayerSpawn");
        // GameObject player = Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

        ElementSelector selector = UI.GetComponent<ElementSelector>();
        if (selector){
            // selector.player = player;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            UI.SetActive(true);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
