using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkieTalkieFix : MonoBehaviour
{
    [SerializeField] GameObject TextE;

    [SerializeField] GameObject TextObjetive;

    [SerializeField] AudioSource _AudioSource;

    bool isd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Walkie"))
        { 
          TextE.SetActive(true);

            print("dd");

            isd = true;
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Walkie"))
        {
            TextE.SetActive(false);

            isd = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isd == true)
        {
            _AudioSource.Play();

            print("Se Hizo");

            TextE.SetActive(false);

            TextObjetive.SetActive(false);
        }

    }
}
