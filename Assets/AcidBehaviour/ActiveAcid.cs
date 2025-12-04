using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAcid : MonoBehaviour
{
    [SerializeField] GameObject DiamondsText;

    [SerializeField] Rigidbody rb;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
          rb.useGravity = true;

            DiamondsText.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DiamondsText.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
