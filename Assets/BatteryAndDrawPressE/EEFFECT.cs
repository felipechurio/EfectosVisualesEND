using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EEFFECT : MonoBehaviour
{
    RaycastHit Info;
    [SerializeField] TextMeshProUGUI PickupE;
    [SerializeField] AudioSource _AudioSource;
    private bool _isPlaying;
    [SerializeField] private Collider _collider;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPlaying = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("PICKEO");

            _isPlaying = true;

           // PickupE.enabled = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPlaying = false;

            PickupE.enabled = false;
        }
    }
    void Update()
    {
       
        if (_isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _isPlaying = false;
                Destroy(_collider);
                StartCoroutine(OnDestroys());
                //PickupE.enabled = false;
            }
        }
    }

    public IEnumerator OnDestroys()
    {
        _AudioSource.Play();
        AmountDrawInstance.Instance.ActualDrawAmount += 1;

        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}


