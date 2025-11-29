using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTurnTv : MonoBehaviour
{
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
        postProcessMaterial.SetFloat("_AlphaIntensity", 0);
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
                    postProcessMaterial.SetFloat("_AlphaIntensity", t);
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
        while (t > 0)
        {
            postProcessMaterial.SetFloat("_AlphaIntensity", t);
            t -= Time.deltaTime / 40f;
            TurnOffTv.gameObject.SetActive(false);
            yield return null;
        }

        if (playerController != null)
            playerController.speedMultiplier = 1f;
    }
}
