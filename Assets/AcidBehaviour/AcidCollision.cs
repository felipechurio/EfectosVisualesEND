using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


public class AcidCollision : MonoBehaviour
{
    [SerializeField] ScriptableRendererFeature acidFeature;
    ScriptableRendererFeature acidFeatureOriginal;


    [SerializeField] Material AcidEffect;


    [SerializeField] GameObject Blood1;

    [SerializeField] GameObject Blood2;

    [SerializeField] GameObject Blood3;

    [SerializeField] GameObject Blood4;

   

    bool InCollison;

    bool Loses;

    float t = 0;

    float t2 = 0;

    float AcidIntensity;


    private void Start()
    {
        acidFeature.SetActive(false);
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("EstaEnElAcido");

            Blood1.SetActive(true);
            Blood2.SetActive(true);
            Blood3.SetActive(true);
            Blood3.SetActive(true);

            Loses = true;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Loses = true;
            acidFeature.SetActive(true);
            AcidEffect.SetFloat("_AcidIntensity", 0.22f);

            InCollison = true;

            StartCoroutine(LerpAcid());
            StartCoroutine(LoseGame());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Loses = false;
            t2 = 0;
            acidFeature.SetActive(false);

            AcidEffect.SetFloat("_AcidIntensity", 0);

            Blood1.SetActive(false);
            Blood2.SetActive(false);
            Blood3.SetActive(false);
            Blood3.SetActive(false);

            StopAllCoroutines();
            InCollison = false;         
        }
    }
    public IEnumerator LerpAcid()
    {
        for (t = 0; t < 0.5f; t += Time.deltaTime)
        {
            AcidEffect.SetFloat("_AcidIntensity", t);
            yield return null;
            if (t > 0.4f) t = 0.5f;
        }
    }

    public IEnumerator LoseGame()
    {     if (Loses)
          {
            for (t2 = 0; t2 < 6f; t2 += Time.deltaTime)
            {
                yield return null;

                print(t2);

                if (t2 > 5f) SceneManager.LoadScene("LoseScreen");
            }

          }
            
    }

   
    private void Update()
    {
        if (Loses == false);
    }
}
