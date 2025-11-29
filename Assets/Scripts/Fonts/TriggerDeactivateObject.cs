using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerDeactivateObject : MonoBehaviour
{
    public TextMeshProUGUI textToFade;
    public GameObject associatedObject; 
    private bool isPlayerInside = false;
    private bool hasInteracted = false;
    private Coroutine fadeLoopCoroutine;

    void Start()
    {
        if (textToFade != null)
        {
            Color c = textToFade.color;
            c.a = 0f;
            textToFade.color = c;
            textToFade.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasInteracted) return;
        if (other.CompareTag("Player"))
        {
            if (associatedObject != null && !associatedObject.activeSelf)
                return; 

            isPlayerInside = true;

            if (textToFade != null)
            {
                textToFade.gameObject.SetActive(true);

                if (fadeLoopCoroutine != null)
                    StopCoroutine(fadeLoopCoroutine);

                fadeLoopCoroutine = StartCoroutine(FadeLoop());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (hasInteracted) return;

        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;

            if (fadeLoopCoroutine != null)
                StopCoroutine(fadeLoopCoroutine);

            StartCoroutine(FadeOutAndDisableText(1f));
        }
    }

    void Update()
    {
        if (hasInteracted) return;

        if (isPlayerInside && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q)))
        {
            if (associatedObject != null)
            {
                associatedObject.SetActive(false);

                Walls wallsScript = FindObjectOfType<Walls>();
                if (wallsScript != null)
                {
                    wallsScript.MarkAsDeactivated(associatedObject);
                }
            }

            if (fadeLoopCoroutine != null)
                StopCoroutine(fadeLoopCoroutine);

            StartCoroutine(FadeOutAndDisableText(1f));

            hasInteracted = true;
            isPlayerInside = false;
        }
    }

    IEnumerator FadeLoop()
    {
        float fadeDuration = 1f;

        while (true)
        {
            yield return FadeText(0f, 1f, fadeDuration);
            yield return FadeText(1f, 0f, fadeDuration);
        }
    }

    IEnumerator FadeText(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color c = textToFade.color;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            textToFade.color = new Color(c.r, c.g, c.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        textToFade.color = new Color(c.r, c.g, c.b, endAlpha);
    }

    IEnumerator FadeOutAndDisableText(float duration)
    {
        yield return FadeText(textToFade.color.a, 0f, duration);
        textToFade.gameObject.SetActive(false);
    }
}
