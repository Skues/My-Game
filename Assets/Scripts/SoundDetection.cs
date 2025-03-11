using UnityEngine;

public class SoundDetection : MonoBehaviour
{
    public float soundRadius = 10f;


    public void CreateSound(Vector3 position, float loudness)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, soundRadius*loudness);
        foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    hitCollider.GetComponent<EnemyAI>().DetectSound(position, loudness);
                }
            }



    }
}
