using UnityEngine;

public class ElementSelector : MonoBehaviour
{
    public GameObject player;

    private GameObject UI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }
    public void ChooseEarth(){
        AttachElement("Earth Power");
        print("EARTHHH");
    }
    public void ChooseLight(){
        AttachElement("Light Power");
        print("LIGHGHGHGHGHT");

    }
    private void AttachElement(string prefab){
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
