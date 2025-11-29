using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWin : MonoBehaviour
{
    [SerializeField] float TimeResetScene;

    public Timer _Timer;

    bool Starts = true;

    private void OnTriggerEnter(Collider other)
    {
        print("Trigger Aceptado");

        if (other.gameObject.CompareTag("Win"))
        {
            if (_Timer.EscapeEnabled == true && Starts == true)
            {
                _Timer.OnVisualGraphActivated();
                StartCoroutine(WinMechanism(TimeResetScene));

                Starts = false;

                AmountDrawInstance.Instance.ActualDrawAmount = 0;
                AmountDrawInstance.Instance.ActualBatteryAmount = 0;

                _Timer.EscapeEnabled = false;

                _Timer.enabled = false;


            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WinMechanism(float Time)
    {
        print("SeHizp");
      yield return new WaitForSeconds(Time);

        SceneManager.LoadScene("Level_1");
    
    }
}
