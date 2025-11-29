using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BatteryAmount : MonoBehaviour
{
    RaycastHit Info;
    [SerializeField] TextMeshProUGUI PickupE;
    [SerializeField] AudioSource _AudioSource;
    [SerializeField] TextMeshProUGUI _BatteryText;
    private bool _isPlaying;

    void Start()
    {
        _BatteryText.text = (AmountDrawInstance.Instance.ActualBatteryAmount + " / 1");
    }

  


    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPlaying = true;
            //PickupE.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPlaying = true;

            PickupE.enabled = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPlaying = false;

            PickupE.enabled = false;
        }
    }
    void Update()
    {
        _BatteryText.text = (AmountDrawInstance.Instance.ActualBatteryAmount + " / 1");

        if (_isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(OnDestroys());
                PickupE.enabled = false;
            }
        }
    }

    public IEnumerator OnDestroys()
    {
        _AudioSource.Play();
        AmountDrawInstance.Instance.ActualDrawAmount += 1;

        print(AmountDrawInstance.Instance.ActualDrawAmount);

        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}




