using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeDamager : MonoBehaviour
{
    [SerializeField] EyeDamager _EyeDamager;

    [SerializeField] EyeLookingAtPlayer _EyeLookingAtPlayer;

    [SerializeField] Material _BloodVignette;

    [SerializeField] PlayerTransparency _PlayerTransparency;

   public bool _IsLooking = false;

    private Coroutine currentCoroutine;


    void Start()
    {
        //_BloodVignette.SetFloat("_IntensityVignette", 0);
    }

    void Update()
    {
        

        float valorActual = _BloodVignette.GetFloat("_IntensityVignette");
       /// Debug.Log("Valor de Intensity Vignette: " + valorActual);


        if (_EyeLookingAtPlayer.LerpVal == 1 && _IsLooking == true)
        {

            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(MiniLerp());
           _IsLooking = false;


        }

        else if (_EyeLookingAtPlayer.LerpVal == 0 && _IsLooking == false)
        {
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(UndoMiniLerp());

            _IsLooking = true;
        }

         //&& _IsLooking == true

        if (_PlayerTransparency.isTransparent == true)
        {
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(UndoMiniLerpTransparency());

        }

       
    }

    public IEnumerator MiniLerp()
    {
        for (float d = 0; d < 1; d += Time.deltaTime)
        {
            
            _BloodVignette.SetFloat("_IntensityVignette", (d * 5));

            yield return null;
        }
        currentCoroutine = null;
    }

  

    public IEnumerator UndoMiniLerp()
    {
        for (float d = 1; d > 0; d -= Time.deltaTime)
        {

            _BloodVignette.SetFloat("_IntensityVignette", (d * 5));

            yield return null;
        }
    }

    public IEnumerator UndoMiniLerpTransparency()
    {
        for (float d = 1; d > 0; d -= Time.deltaTime/2)
        {
            _BloodVignette.SetFloat("_IntensityVignette", d );

            yield return null;
        }
        _IsLooking = true;
    }
}
