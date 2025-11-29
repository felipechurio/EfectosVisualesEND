using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BatteryDrawUI : MonoBehaviour
{
    [SerializeField] GameObject BatteryUI;

    [SerializeField] TextMeshProUGUI DrawUI;

    [SerializeField] GameObject DrawIamge;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BatteryUI.SetActive(true);

            DrawUI.gameObject.SetActive(true);

            DrawIamge.SetActive(true);

        }
    }

    private void Update()
    {
        AmountDrawInstance.Instance.ActualDrawAmount = Mathf.Clamp(AmountDrawInstance.Instance.ActualDrawAmount, 0, 3);
        DrawUI.text = AmountDrawInstance.Instance.ActualDrawAmount + " / 3";

    }
}
