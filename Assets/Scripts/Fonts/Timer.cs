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
    [SerializeField, Tooltip("Tiempo inicial del contador en segundos")]
    private float timerTime = 300f; // 5 minutos

    [Header("Visual Graph y Partículas")]
    public GameObject requiredParticles;
    public GameObject visualGraph;

    [Header("Win UI y Sonido")]
    public Canvas winCanvas;
    public TMP_Text winText;
    public AudioSource winSound;
    public float fadeDuration = 1f;

    [Header("Win Trigger")]
    public Collider winTrigger;

    [Header("Elevator Control After Win")]
    public ElevatorController elevatorController;

    [Header("Trigger de inicio del timer")]
    [SerializeField] private Collider _Collider;

    private bool timerActive = false;
    private bool visualGraphActivated = false;
    private float currentTime;

    #region Flags originales
    public AudioSource ElevatorDing;
    public bool EscapeEnabled;
    private bool ElevatorDings = true;
    #endregion

    void Start()
    {
        timerCanvas.gameObject.SetActive(false);
        if (visualGraph != null) visualGraph.SetActive(false);
        if (winCanvas != null) winCanvas.gameObject.SetActive(false);
        if (winTrigger != null) winTrigger.enabled = false;

        currentTime = timerTime;
    }

    void Update()
    {
        if (timerActive)
        {
            // Contar hacia atrás
            currentTime -= Time.deltaTime;
            if (currentTime < 0) currentTime = 0;

            // Mostrar en UI
            int minutes = (int)(currentTime / 60);
            int seconds = (int)(currentTime % 60);
            int cents = (int)((currentTime - (int)currentTime) * 100);
            timerText.text = $"{minutes:00}:{seconds:00}:{cents:00}";

            // Activar EscapeEnabled si visualGraph activo
            if (visualGraph != null && visualGraph.activeSelf && !visualGraphActivated)
            {
                EscapeEnabled = true;

                if (ElevatorDings) ElevatorDing.Play();
                ElevatorDings = false;
            }
            else EscapeEnabled = false;

            // Fin del timer
            if (currentTime <= 0 && !visualGraphActivated)
            {
                timerActive = false;
                loopSound.Stop();
                timerCanvas.gameObject.SetActive(false);
                SceneManager.LoadScene("LoseScreen"); // reinicia la escena
            }
        }

        // Activar WinTrigger cuando requiredParticles y visualGraph estén activos
        if (requiredParticles != null && requiredParticles.activeSelf &&
            visualGraph != null && visualGraph.activeSelf &&
            winTrigger != null && !winTrigger.enabled)
        {
            winTrigger.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Activar timer al tocar el trigger original
        if (other.CompareTag("Player"))
        {
            if (!timerActive)
            {
                StartTimerSequence();
            }

            // Activar WinTrigger si corresponde
            if (winTrigger != null && winTrigger.enabled)
            {
                ActivarWin();
            }
        }
    }

    private void StartTimerSequence()
    {
        if (_Collider != null)
            _Collider.enabled = false;

        if (timerCanvas != null)
            timerCanvas.gameObject.SetActive(true);

        if (loopSound != null)
            loopSound.Play();

        timerActive = true;
        currentTime = timerTime;

        if (requiredParticles != null)
            requiredParticles.SetActive(true);
        if (visualGraph != null)
            visualGraph.SetActive(true);

        visualGraphActivated = false;
        ElevatorDings = true;
    }

    public void ActivarWin()
    {
        // Ejecutar secuencia de Win UI y sonido
       // StartCoroutine(WinSequence());


        // Hacer que el ascensor baje automáticamente
        //if (elevatorController != null)
       // {
            elevatorController.StartElevatorDescent();
       // }
    }

    private IEnumerator WinSequence()
    {
        if (winCanvas != null) winCanvas.gameObject.SetActive(true);

        // Fade-in del WinText
        if (winText != null)
        {
            Color startColor = winText.color;
            winText.color = new Color(startColor.r, startColor.g, startColor.b, 0);
            float t = 0;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                winText.color = new Color(startColor.r, startColor.g, startColor.b, t / fadeDuration);
                yield return null;
            }
        }

        // Reproducir WinSound
        if (winSound != null) winSound.Play();

        // Esperar mientras se reproduce la música
        yield return new WaitForSeconds(7f);

        // Fade-out del WinText
        if (winText != null)
        {
            Color startColor = winText.color;
            float t = 0;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                winText.color = new Color(startColor.r, startColor.g, startColor.b, 1 - t / fadeDuration);
                yield return null;
            }
        }

        if (winCanvas != null) winCanvas.gameObject.SetActive(false);
    }
}
