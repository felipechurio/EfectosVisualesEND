using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Material _BloodVignette;
    [SerializeField] EyeDamager _EyeDamager;
    [SerializeField] PlayerTransparency _PlayerTransparency;
    private Vector3 PlayerStartPosition;
    float VignetteIntensity;

    private bool IsLooking;

    float t;
    // Start is called before the first frame update
    void Start()
    {
        PlayerStartPosition = Player.transform.position;

        VignetteIntensity = _BloodVignette.GetFloat("_IntensityVignette");
    }

    // Update is called once per frame
    void Update()
    {
        if (VignetteIntensity > 6 && _EyeDamager._IsLooking == true)
        {
          for ( t = 0; t < 5; t+= Time.deltaTime)
          {

                print(t);
            
          }

          if (t == 5 || t > 4)
          {
             Player.transform.position = PlayerStartPosition;
             
          }

        }

        
    }
}
