using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class FlareEffect : MonoBehaviour
{
    [Header("References")]
    public GameObject flareVFX;        
    public AudioSource activateSound;
    public GameObject activateCanvas;
    public GameObject activateText;
    public GameObject winCanvas;
    public GameObject winText;
    public AudioSource winAudio;
    public Timer timer;

    [Header("Diamonds")]
    public List<GameObject> diamonds;   
    public int requiredInactiveDiamonds = 8;

    private bool playerInside = false;
    private bool hasTriggered = false;

    void Start()
    {
        if (flareVFX != null) flareVFX.SetActive(false);
        if (activateCanvas != null) activateCanvas.SetActive(false);
        if (activateText != null) activateText.SetActive(false);
        if (winCanvas != null) winCanvas.SetActive(false);
        if (winText != null) winText.SetActive(false);
    }

    void Update()
    {
        if (!playerInside) return;

        bool diamondsInactive = AreDiamondsInactive();

        if (activateCanvas != null) activateCanvas.SetActive(diamondsInactive);
        if (activateText != null) activateText.SetActive(diamondsInactive);

        if (!hasTriggered && diamondsInactive && Input.GetKeyDown(KeyCode.Q))
        {
            hasTriggered = true;
            StartCoroutine(ActivateSequence());
        }
    }

    private bool AreDiamondsInactive()
    {
        int inactiveCount = 0;
        foreach (GameObject diamond in diamonds)
        {
            if (!diamond.activeInHierarchy)
                inactiveCount++;
        }
        return inactiveCount >= requiredInactiveDiamonds;
    }

    private IEnumerator ActivateSequence()
    {
        if (flareVFX != null)
        {
            flareVFX.SetActive(true);
            VisualEffect vfx = flareVFX.GetComponent<VisualEffect>();
            if (vfx != null)
            {
                vfx.Play();
            }
        }

        if (activateSound != null)
            activateSound.Play();

        yield return new WaitForSeconds(7f);

        if (winCanvas != null) winCanvas.SetActive(true);
        if (winText != null) winText.SetActive(true);
        if (winAudio != null) winAudio.Play();
        StartCoroutine(WinScene());
    }

    private IEnumerator WinScene()
    {
        timer.enabled = false;
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("WinCanvas");
    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("Player entered trigger.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            if (activateCanvas != null) activateCanvas.SetActive(false);
            if (activateText != null) activateText.SetActive(false);
            Debug.Log("Player exited trigger.");
        }
    }
}
