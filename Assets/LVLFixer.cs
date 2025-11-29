using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVLFixer : MonoBehaviour
{
    [SerializeField] Collider BoxTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AmountDrawInstance.Instance.ActualDrawAmount == 3)
        {
            BoxTrigger.enabled = true;
        }

        else if (AmountDrawInstance.Instance.ActualDrawAmount != 3)
        { 
         BoxTrigger.enabled=false;
        }
    }
}
