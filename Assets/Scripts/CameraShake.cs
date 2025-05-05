using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Shake(float duration, float magnitude){
        print("CAMERA SHAKE");
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0f;
        while (elapsed < duration){
            float x = Random.Range(-1f, 1f)*magnitude;
            float y = Random.Range(-1f, 1f)*magnitude;
            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;

        }
    transform.localPosition = originalPos;
    }
}
