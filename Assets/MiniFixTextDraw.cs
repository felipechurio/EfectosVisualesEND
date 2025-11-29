using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniFixTextDraw : MonoBehaviour
{
     public TextMeshProUGUI m_TextMeshProUGUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_TextMeshProUGUI.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_TextMeshProUGUI.enabled = false;
        }

    }
}
