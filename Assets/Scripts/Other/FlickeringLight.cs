using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] List<GameObject> Lights = new List<GameObject>();

    [SerializeField] List<GameObject> Lights2 = new List<GameObject>();


    public Light pointLight;

    public float minIntensity = 0.2f;
    public float maxIntensity = 2.0f;

    public float minFlickerTime = 0.05f;
    public float maxFlickerTime = 0.3f;

    private bool lightOn = true;

    void Start()
    {
        if (pointLight == null)
            pointLight = GetComponent<Light>();

        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (lightOn)
        {
            pointLight.intensity = Random.Range(minIntensity, maxIntensity);
            float waitTime = Random.Range(minFlickerTime, maxFlickerTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine (TurnOffAllLights());
        }
    }

   // private void OnTriggerEnter(Collider other)
   // {
       // if (other.CompareTag("Trigger"))
       // {
            //StartCoroutine (TurnOffAllLights());
       // }
   // }

    private IEnumerator TurnOffAllLights()
    {
        foreach (GameObject obj in Lights)
        {
            Renderer rend = obj.GetComponent<Renderer>();


            if (rend != null)
            {
                Material mat = rend.material;

                rend.material.color = Color.white;
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", Color.black);
            }

            yield return new WaitForSeconds(0.1f);
        }

        foreach (GameObject obj in Lights2)
        {
            obj.SetActive(false);

            yield return new WaitForSeconds(0.1f);
        }

    }

}
