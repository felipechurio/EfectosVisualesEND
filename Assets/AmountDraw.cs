using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AmountDrawInstance : MonoBehaviour
{
    public static AmountDrawInstance Instance;

    public int ActualDrawAmount = 0;

    public int ActualBatteryAmount = 0;


    public TextMeshProUGUI _DrawAmount;

    public TextMeshProUGUI _BatteryAmount;

    private string lastSceneName = "Level_1";


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (ActualDrawAmount == 3)
        {
            _DrawAmount.color = Color.green;
        }

        if (ActualBatteryAmount == 1)
        {
            _BatteryAmount.color = Color.green;
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == lastSceneName)
        {
            //Debug.Log("🔥 La escena se reinició.");
        }
        else
        {
            //Debug.Log("🔄 Nueva escena cargada.");
        }

        lastSceneName = scene.name;


        if (scene.name == "lvl1")
        {
            Debug.Log("Entré a lvl1");

            ActualDrawAmount = 0;

            ActualBatteryAmount = 0;

        }
    }
}
