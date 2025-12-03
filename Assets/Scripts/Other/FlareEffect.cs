using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FlareEffect : MonoBehaviour
{
    [Header("References")]
    public GameObject flareVFX;         // Visual Graph / Flare
    public AudioSource activateSound;
    public GameObject activateCanvas;
    public GameObject activateText;
    public GameObject winCanvas;
    public GameObject winText;
    public AudioSource winAudio;

    [Header("Diamonds")]
    public List<GameObject> diamonds;   // List of 8 diamonds
    public int requiredInactiveDiamonds = 8;

    private bool playerInside = false;
    private bool hasTriggered = false;

    void Start()
    {
        // Start all objects disabled
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

        // Activar o desactivar activateCanvas y activateText dinámicamente
        if (activateCanvas != null) activateCanvas.SetActive(diamondsInactive);
        if (activateText != null) activateText.SetActive(diamondsInactive);

        // Secuencia completa solo si Q presionado y no ha sido activado antes
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
        // Activar y reproducir el VFX
        if (flareVFX != null)
        {
            flareVFX.SetActive(true);
            VisualEffect vfx = flareVFX.GetComponent<VisualEffect>();
            if (vfx != null)
            {
                vfx.Play();
            }
        }

        // Activar sonido de "activate"
        if (activateSound != null)
            activateSound.Play();

        // Esperar 7 segundos
        yield return new WaitForSeconds(7f);

        // Activar win canvas, win text y win audio
        if (winCanvas != null) winCanvas.SetActive(true);
        if (winText != null) winText.SetActive(true);
        if (winAudio != null) winAudio.Play();
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
            // Opcional: desactivar activateCanvas/text al salir
            if (activateCanvas != null) activateCanvas.SetActive(false);
            if (activateText != null) activateText.SetActive(false);
            Debug.Log("Player exited trigger.");
        }
    }
}
