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

    //[SerializeField]  Material BackupMaterial;



    [SerializeField] GameObject Blood1;

    [SerializeField] GameObject Blood2;

    [SerializeField] GameObject Blood3;

    [SerializeField] GameObject Blood4;

   

    bool InCollison;

    bool Loses;

    float t = 0;

    float AcidIntensity;


    private void Start()
    {
        

        // acidFeatureOriginal = acidFeature;  // guardo referencia original
        acidFeature.SetActive(false);


        //AcidIntensity = AcidEffect.GetFloat("_AcidPower");

        //AcidEffect.SetFloat("_AcidIntensity", 0);
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


            //AcidEffect.SetFloat("_AcidIntensity", 0.5f);

            //InCollison = true;

            //StopAllCoroutines();
            //StartCoroutine(LerpAcid());
            //t = 0.5f;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //AcidEffect = BackupMaterial;

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
            acidFeature.SetActive(false);

            AcidEffect.SetFloat("_AcidIntensity", 0);

            Blood1.SetActive(false);
            Blood2.SetActive(false);
            Blood3.SetActive(false);
            Blood3.SetActive(false);

            StopCoroutine(LerpAcid());
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
    {     
            for (float d = 0; d < 3f; d += Time.deltaTime)
            {
                yield return null;
               // AcidEffect.SetFloat("_AcidIntensity", 0);

                if (d > 2.9f) SceneManager.LoadScene("Level_1");

            } 
    }

   
    private void Update()
    {
        if (Loses == false) StopCoroutine(LoseGame());


        

    }
}
