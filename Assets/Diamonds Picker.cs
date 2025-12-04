using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DiamondsPicker : MonoBehaviour
{
    public int DiamondsAmount = 0;

    [SerializeField] TextMeshProUGUI DiamondsAmounts;

    [SerializeField] AudioSource AudioSource;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Diamond"))
        {   
            AudioSource.Play();
            DiamondsAmount += 1;

           collision.gameObject.SetActive(false);
        
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DiamondsAmounts.text = (DiamondsAmount.ToString() + " / " + "8");
    }
}
