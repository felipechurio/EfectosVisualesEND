using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EEFFECT2 : MonoBehaviour
{
    RaycastHit Info;
    // [SerializeField] GameObject EText;
    [SerializeField] TextMeshProUGUI BatteryAmount;

    [SerializeField] TextMeshProUGUI PickupE;
    [SerializeField] AudioSource _AudioSource;
    [SerializeField] Collider _collider;
    private bool _isPlaying;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPlaying = true;
            //EText.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            _isPlaying = true;

            //PickupE.enabled = true;

           // EText.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPlaying = false;

            PickupE.enabled = false;

           // EText.SetActive(false);
        }
    }
    void Update()
    {
       
        if (_isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _isPlaying = false;
                _collider.enabled = false;
                StartCoroutine(OnDestroys());
                PickupE.enabled = false;
              //  AmountDrawInstance.Instance.ActualBatteryAmount += 1;
               // EText.SetActive(true);
               // Destroy(EText);
            }
        }
    }

    public IEnumerator OnDestroys()
    {
        _AudioSource.Play();
       // AmountDrawInstance.Instance.ActualBatteryAmount += 1;

        print(AmountDrawInstance.Instance.ActualBatteryAmount);

        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        AmountDrawInstance.Instance.ActualBatteryAmount += 1;
        BatteryAmount.text = (AmountDrawInstance.Instance.ActualBatteryAmount + " / 1");
        
        //print(AmountDrawInstance.Instance.ActualBatteryAmount);
    }
}


