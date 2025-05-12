using UnityEngine;
using UnityEngine.UI;
public class PlayerElements : MonoBehaviour
{
    public float earthResource = 0f;
    public float lightResource = 0f;
    public float maxResource = 100f;
    public Slider earthSlider; // UI element for Earth
    public Slider lightSlider; // UI element for Light

    private bool isOnEarthArea = false;
    private bool isInLightArea = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnEarthArea)
        {
            RegenerateResource(ref earthResource);
        }
        if (isInLightArea)
        {
            RegenerateResource(ref lightResource);
        }
        
        UpdateUI();
    }
    void RegenerateResource(ref float resource)
    {
        if (resource < maxResource)
            resource += Time.deltaTime * 10f; // Regeneration speed
    }

    void UpdateUI()
    {
        earthSlider.value = earthResource / maxResource;
        lightSlider.value = lightResource / maxResource;
    }

    // Trigger enter/exit methods for areas
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EarthZone"))
            isOnEarthArea = true;
        if (other.CompareTag("LightZone"))
            isInLightArea = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EarthZone"))
            isOnEarthArea = false;
        if (other.CompareTag("LightZone"))
            isInLightArea = false;
    }

}
