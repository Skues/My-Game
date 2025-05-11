using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light lightSource;
    public float minIntensity = 1.5f;
    public float maxIntensity = 4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lightSource.intensity = Random.Range(minIntensity, maxIntensity);
    }
}
