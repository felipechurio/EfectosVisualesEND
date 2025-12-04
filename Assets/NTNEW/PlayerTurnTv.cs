using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerTurnTv : MonoBehaviour
{
    [SerializeField] ScriptableRendererFeature CameraFeature;
    ScriptableRendererFeature CameraFeatureOriginal;
    [SerializeField] GameObject Player;
    [SerializeField] Material TvEstatic;
    [SerializeField] TextMeshProUGUI TurnOffTv;
    [SerializeField] AudioSource _AudioSource;
    [SerializeField] Material _Black;
    [SerializeField] Material postProcessMaterial;

    [SerializeField] PlayerController playerController; 
    [SerializeField] float slowFactor = 0.5f; 

    private Renderer _Renderer;
    private RaycastHit Info;
    private float t = 0.28f;

    public bool _isPlaying = false;

    private void Start()
    {
        _Renderer = GetComponent<Renderer>();
        _Renderer.material = _Black;
        //postProcessMaterial.SetFloat("_AlphaIntensity", 0);
        CameraFeature.SetActive(false);

    }

    private void Update()
    {
        Debug.DrawRay(Player.transform.position + Vector3.up, Player.transform.forward * 10, Color.red);

        if (Physics.Raycast(Player.transform.position + new Vector3(0, 3, 0), Player.transform.forward, out Info, 10f))
        {
            if (Info.collider.CompareTag("Tv"))
            {
                TurnOffTv.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Info.collider.GetComponent<Renderer>().material = TvEstatic;
                    CameraFeature.SetActive(true);
                    _AudioSource.Play();

                    if (playerController != null)
                        playerController.speedMultiplier = slowFactor;

                    StartCoroutine(UnableEffect());
                    this.enabled = false;
                }

                _isPlaying = true;
            }
            else
            {
                _isPlaying = false;
            }
        }
        else
        {
            _isPlaying = false;
        }

        if (!_isPlaying)
        {
            TurnOffTv.gameObject.SetActive(false);
        }
    }

    public IEnumerator UnableEffect()
    {
       yield return new WaitForSeconds(5);
            CameraFeature.SetActive(false);
            TurnOffTv.gameObject.SetActive(false);

        if (playerController != null)
            playerController.speedMultiplier = 1f;


    }

        
    
}
