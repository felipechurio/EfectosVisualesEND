using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidOffStart : MonoBehaviour
{
    [SerializeField] Material AcidEffect;
    [SerializeField] float AcidIntensity;
    // Start is called before the first frame update
    void Start()
    {
        AcidIntensity = AcidEffect.GetFloat("_AcidPower");
        AcidEffect.SetFloat("_AcidIntensity", 0);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
