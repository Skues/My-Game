using System.Collections;
using System.Security;
using Unity.Mathematics;
using UnityEngine;

public class DoorController : MonoBehaviour, IDoor
{
    private bool isOpen = false;
    private float rotationSpeed = 1f;
    public float openAngle = -90f;
    
    public Quaternion openRotation;
    public Quaternion closedRotation;
    private Quaternion targetRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));

    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime*rotationSpeed);
    }
    public void ToggleDoor(){
        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(RotateDoor());
    }
    private IEnumerator RotateDoor(){
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }
        transform.rotation = targetRotation;

    }
}
