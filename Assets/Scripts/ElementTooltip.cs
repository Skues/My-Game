using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ElementTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string description;
    public GameObject tooltipPanel;
    public TextMeshProUGUI tooltipText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnPointerEnter(PointerEventData eventData){
        tooltipPanel.SetActive(true);
        tooltipText.text = description;
        tooltipPanel.transform.position = Input.mousePosition;
    }
    public void OnPointerExit(PointerEventData eventData){
        tooltipPanel.SetActive(false);
    }
    void OnDisable(){
        tooltipPanel.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
