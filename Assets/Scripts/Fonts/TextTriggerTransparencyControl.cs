using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTriggerTransparencyControl : MonoBehaviour
{
    public TextMeshProUGUI textToFade;

    private bool isPlayerInside = false;
    private bool hasPressedT = false;
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

    void Update()
    {
        if (hasPressedT || !isPlayerInside || textToFade == null)
            return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            hasPressedT = true;

            if (fadeLoopCoroutine != null)
                StopCoroutine(fadeLoopCoroutine);

            StartCoroutine(FadeOutAndDisableText(1f));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasPressedT || textToFade == null)
            return;

        if (other.CompareTag("Player") && CompareTag("TextTrigger"))
        {
            isPlayerInside = true;

            textToFade.gameObject.SetActive(true);

            if (fadeLoopCoroutine != null)
                StopCoroutine(fadeLoopCoroutine);

            fadeLoopCoroutine = StartCoroutine(FadeLoop());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && CompareTag("TextTrigger"))
        {
            isPlayerInside = false;

            if (fadeLoopCoroutine != null)
                StopCoroutine(fadeLoopCoroutine);

            StartCoroutine(FadeOutAndDisableText(1f));
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
