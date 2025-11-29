using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class TriggerEffectsSequence : MonoBehaviour
{
    [Header("Trigger Settings")]
    public string playerTag = "Player"; 

    [Header("Effects and Sounds")]
    public AudioClip firstClip;        
    public VisualEffect firstVFX;     
    public AudioClip secondClip;       
    public VisualEffect secondVFX;     

    [Header("Timing")]
    public float delayBetweenEffects = 2f; 

    private bool isPlayerInside = false;   
    private bool hasActivated = false;     
    private AudioSource tempAudioSource;   

    void Update()
    {
        if (!isPlayerInside || hasActivated)
            return;

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
        {
            hasActivated = true;
            StartCoroutine(PlayEffectsSequence());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
            isPlayerInside = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
            isPlayerInside = false;
    }

    private IEnumerator PlayEffectsSequence()
    {

        if (firstClip != null)
        {
            tempAudioSource = gameObject.AddComponent<AudioSource>();
            tempAudioSource.clip = firstClip;
            tempAudioSource.Play();
        }

        if (firstVFX != null)
        {
            firstVFX.gameObject.SetActive(true);
            firstVFX.Play();
        }

        yield return new WaitForSeconds(delayBetweenEffects);

        if (tempAudioSource != null)
        {
            tempAudioSource.Stop();
            Destroy(tempAudioSource);
        }

        if (firstVFX != null)
            firstVFX.Stop();

        if (secondClip != null)
        {
            tempAudioSource = gameObject.AddComponent<AudioSource>();
            tempAudioSource.clip = secondClip;
            tempAudioSource.Play();
        }

        if (secondVFX != null)
        {
            secondVFX.gameObject.SetActive(true);
            secondVFX.Play();
        }
    }
}
