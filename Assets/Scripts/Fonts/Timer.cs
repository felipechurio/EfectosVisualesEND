using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
   [Header("UI y Audio")]
    [SerializeField] private Canvas timerCanvas;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private AudioSource loopSound;

    [Header("Configuración del temporizador")]
    [SerializeField, Tooltip("Tiempo inicial del contador (segundos)")]
    private float timerTime = 10f;

    [Header("Visual Graph y Partículas")]
    [SerializeField] private GameObject requiredParticles;
    [SerializeField] private GameObject visualGraph;

    [Header("Win UI y Sonido")]
    [SerializeField] private Canvas winCanvas;       // canvas de "You Win"
    [SerializeField] private TMP_Text winText;        // texto dentro del canvas
    [SerializeField] private AudioSource winSound;    // sonido de victoria
    [SerializeField, Tooltip("Duración del fade in del texto")]
    private float fadeDuration = 1f;

    private bool timerActive = false;
    private bool visualGraphActivated = false;
    private float currentTime;

    
    public AudioSource ElevatorDing;

    public bool EscapeEnabled;
    private bool ElevatorDings = true;

    [SerializeField] Collider _Collider;

    void Start()
    {
        timerCanvas.gameObject.SetActive(false);
        if (visualGraph != null)
            visualGraph.SetActive(false);

        if (winCanvas != null)
            winCanvas.gameObject.SetActive(false);

        currentTime = timerTime;
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0) currentTime = 0;

            int minutes = (int)(currentTime / 60);
            int seconds = (int)(currentTime % 60);
            int cents = (int)((currentTime - (int)currentTime) * 100);
            timerText.text = $"{minutes:00}:{seconds:00}:{cents:00}";

            if (visualGraph != null && visualGraph.activeSelf && !visualGraphActivated)
            {
                //OnVisualGraphActivated();

                EscapeEnabled = true;


                if (ElevatorDings) ElevatorDing.Play();

                ElevatorDings = false;

                print("ElevatorDings");
            }

            else EscapeEnabled = false;

            if (currentTime <= 0 && !visualGraphActivated)
            {
                timerActive = false;
                loopSound.Stop();
                timerCanvas.gameObject.SetActive(false);
                Debug.Log("Game Over");
                SceneManager.LoadScene("Level_1");
            }
        }
    }

    public void OnVisualGraphActivated()
    {
        visualGraphActivated = true;
        timerActive = false;
        loopSound.Stop();
        timerCanvas.gameObject.SetActive(false);

        if (requiredParticles != null)
            requiredParticles.SetActive(false);

        Debug.Log("You Win");

        // Activar canvas de Win y fade in
        if (winCanvas != null && winText != null)
        {
            winCanvas.gameObject.SetActive(true);
            winText.color = new Color(winText.color.r, winText.color.g, winText.color.b, 0); // transparente
            StartCoroutine(FadeInText(winText, fadeDuration));
        }

        // Reproducir sonido de victoria
        if (winSound != null)
            winSound.Play();
    }

    private IEnumerator FadeInText(TMP_Text text, float duration)
    {
        float elapsed = 0f;
        Color c = text.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / duration);
            text.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        text.color = new Color(c.r, c.g, c.b, 1f); // asegurar que quede totalmente visible
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timerCanvas.gameObject.SetActive(true);
            loopSound.Play();
            timerActive = true;
            currentTime = timerTime;

            visualGraphActivated = false;
            if (requiredParticles != null)
                requiredParticles.SetActive(true);
            if (visualGraph != null)
                visualGraph.SetActive(false);

            _Collider.enabled = false;
            // Desactivo el Collider para que no se reinicie el tiempo
        }
    }
}
