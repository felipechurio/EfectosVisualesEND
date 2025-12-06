using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHSController : MonoBehaviour
{

    [SerializeField] List<HologramEntorno> Materials = new List<HologramEntorno>(); 

    public Material mat;

    public float targetNoiseAmount = 67f;
    public float targetVHSStrenght = 18.8f;
    public float targetScanLinesStrenght = 0.558f;

    public float duration = 2f; 

    private bool isActive = false; 

    void Start()
    {
       


        if (mat != null)
        {
            mat.SetFloat("_NoiseAmount", 0f);
            mat.SetFloat("_VHSStrenght", 0f);
            mat.SetFloat("_ScanLinesStrenght", 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive && mat != null)
        {
            StartCoroutine(VHSEffectCoroutine());

            foreach (HologramEntorno it in Materials)
            { 
              it.enabled = true;
            }
        }
    }

    private IEnumerator VHSEffectCoroutine()
    {
        isActive = true;

        float startNoise = 0f;
        float startVHS = 0f;
        float startScanLines = 1f;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            mat.SetFloat("_NoiseAmount", Mathf.Lerp(startNoise, targetNoiseAmount, t));
            mat.SetFloat("_VHSStrenght", Mathf.Lerp(startVHS, targetVHSStrenght, t));
            mat.SetFloat("_ScanLinesStrenght", Mathf.Lerp(startScanLines, targetScanLinesStrenght, t));

            yield return null;
        }

        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            mat.SetFloat("_NoiseAmount", Mathf.Lerp(targetNoiseAmount, 0f, t));
            mat.SetFloat("_VHSStrenght", Mathf.Lerp(targetVHSStrenght, 0f, t));
            mat.SetFloat("_ScanLinesStrenght", Mathf.Lerp(targetScanLinesStrenght, 1f, t));

            yield return null;
        }

        isActive = false;
    }
}
