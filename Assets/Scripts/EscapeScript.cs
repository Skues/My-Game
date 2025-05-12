using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using Unity.Mathematics;


public class EscapeScript : MonoBehaviour
{
    public Transform explosionTarget;
    public CanvasGroup canvasGroup;
    public ParticleSystem explosionEffect;
    public AudioSource explosionSound;
    public float explosionDuration;
    private float fadeDuration = 2f;
    private bool triggered = false;
    public CameraShake cameraShake;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            Debug.Log("GOGOGOGOGOGOGO");
            StartCoroutine(cameraShake.Shake(3f, 0.4f));
            // StartCoroutine(FadeIn());
            StartCoroutine(PlayExplosion());
            triggered = true;
            Debug.Log("Player entered the area!");

        }
    }
    private IEnumerator PlayExplosion(){
        Camera mainCam = Camera.main;
        Quaternion initalRot = player.transform.rotation;
        Vector3 direction = (explosionTarget.position - player.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        player.GetComponent<PlayerMovement>().enabled = false;
        float t =0f;
        while (t < 1f){
            t += Time.deltaTime*1f;
            player.transform.rotation = Quaternion.Slerp(initalRot, targetRotation, t);
            yield return null;
        }
        explosionEffect.Play();
        explosionSound.Play();

        yield return new WaitForSeconds(explosionDuration);
        player.GetComponent<PlayerMovement>().enabled = true;

    }

    private IEnumerator FadeIn(){
        float elapsed = 0f;
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
    }
    // Update is called once per frame
    }

