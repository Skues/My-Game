using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class EscapeScript : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private float fadeDuration = 2f;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            Debug.Log("GOGOGOGOGOGOGO");
            StartCoroutine(FadeIn());
            triggered = true;
            Debug.Log("Player entered the area!");

        }
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

