using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class PlayerTransparency : MonoBehaviour
{
    public float fadeDuration = 2f;
    public Transform dummyTarget; 

    private Material runtimeMaterial;
    public bool isFading = false;
    public bool isTransparent = false;

    void Start()
    {
        runtimeMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !isFading)
        {
            if (isTransparent)
                StartCoroutine(FadeToAlpha(1f));
            else
                StartCoroutine(FadeToAlpha(0f));
        }

        if (!isTransparent && !isFading && dummyTarget != null)
        {
            dummyTarget.position = transform.position;
        }
    }

    private IEnumerator FadeToAlpha(float targetAlpha)
    {
        isFading = true;
        
        if (targetAlpha == 0f)
            isTransparent = true;

        Color startColor = runtimeMaterial.GetColor("_Base_Color");
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        float time = 0f;
        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            Color lerpedColor = Color.Lerp(startColor, endColor, t);
            runtimeMaterial.SetColor("_Base_Color", lerpedColor);
            time += Time.deltaTime;
            yield return null;
        }

        runtimeMaterial.SetColor("_Base_Color", endColor);

        if (targetAlpha == 1f)
            isTransparent = false;

        isFading = false; ;
    }
}
