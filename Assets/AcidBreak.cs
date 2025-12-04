using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBreak : MonoBehaviour
{
    [SerializeField] AudioSource _AudioSource;
    [SerializeField] GameObject _Acid;
    [SerializeField] GameObject _BreakedGlass;
    [SerializeField] GameObject _BreakedGlass2;
    [SerializeField] GameObject _BreakedGlass3;
    [SerializeField] GameObject _BreakedGlass4;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        { 
          _AudioSource.Play();
            _Acid.SetActive(true);
            _BreakedGlass.SetActive(true);
            _BreakedGlass2.SetActive(true);
            _BreakedGlass3.SetActive(true);
            _BreakedGlass4.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _Acid.SetActive(false);
        _BreakedGlass.SetActive(false);
        _BreakedGlass2.SetActive(false);
        _BreakedGlass3.SetActive(false);
        _BreakedGlass4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
