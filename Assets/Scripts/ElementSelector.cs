using UnityEngine;

public class ElementSelector : MonoBehaviour
{
    public GameObject playerPrefab;

    private GameObject UI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }
    public void ChooseEarth(){
        print("EARTHHH");

        AttachElement("Earth Power");
    }
    public void ChooseLight(){
        print("LIGHGHGHGHGHT");

        AttachElement("Light Power");

    }
    private void AttachElement(string prefab){
        GameObject startupCam = GameObject.Find("StartupCamera");
        GameObject spawnPoint = GameObject.FindWithTag("PlayerSpawn");
        if (startupCam){
            startupCam.SetActive(false);
        }
        GameObject player = Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        Transform element = player.transform.Find(prefab);
        element.gameObject.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
