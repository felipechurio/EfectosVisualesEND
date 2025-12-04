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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        { 
          _AudioSource.Play();
            _Acid.SetActive(true);
            _BreakedGlass.SetActive(true);
            _BreakedGlass2.SetActive(true);
            _BreakedGlass3.SetActive(true);
            _BreakedGlass4.SetActive(true);
            StartCoroutine(Disablethis());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisableAcid());
        _BreakedGlass.SetActive(false);
        _BreakedGlass2.SetActive(false);
        _BreakedGlass3.SetActive(false);
        _BreakedGlass4.SetActive(false);
    }

    public IEnumerator DisableAcid()
    {
        yield return new WaitForSeconds(2);
        _Acid.SetActive(false);

    }

    public IEnumerator Disablethis()
    { 
      yield return new WaitForSeconds (2);
            this.enabled = false;
        Destroy(this.gameObject);
    }
}
