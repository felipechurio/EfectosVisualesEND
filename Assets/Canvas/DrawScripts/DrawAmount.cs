using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DrawAmount : MonoBehaviour
{
   // public TextMeshProUGUI m_TextMeshProUGUI;

    public AudioSource AudioSource;
    void Start()
    {
       // m_TextMeshProUGUI.text = (AmountDrawInstance.Instance.ActualDrawAmount  + " / 3");
       // m_TextMeshProUGUI.enabled = false;
    }

    public IEnumerator DisableItem()
    { 
      yield return new WaitForSeconds(0.7f);

      this.gameObject.SetActive(false);
    }
}
