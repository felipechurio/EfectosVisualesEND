using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalDebug : MonoBehaviour
{
    [Header("Crystal to deactivate")]
    public List<GameObject> Crystal; 

    [Header("Test Key")]
    public KeyCode deactivateKey = KeyCode.O; 

    void Update()
    {
        if (Input.GetKeyDown(deactivateKey))
        {
            foreach (GameObject diamond in Crystal)
            {
                if (Crystal != null)
                    diamond.SetActive(false);
            }

            Debug.Log("All Crystal deactivated for testing!");
        }
    }
}
