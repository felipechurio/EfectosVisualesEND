using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryLvl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoseTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private IEnumerator LoseTime()
    {
        AmountDrawInstance.Instance.ActualBatteryAmount = 0;
        AmountDrawInstance.Instance.ActualDrawAmount = 0;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Level_1");

    }
}
