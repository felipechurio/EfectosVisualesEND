using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramEntorno : MonoBehaviour
{
    [SerializeField] Material _Material;

    [SerializeField] Material _Material2;

    Renderer _Renderer;

    // Start is called before the first frame update
    void Start()
    {
        _Renderer = GetComponent<Renderer>();

        StartCoroutine(Renderers());
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Renderers()
    { 
      _Renderer.material = _Material;
        yield return new WaitForSeconds(10);
        _Renderer.material = _Material2;
    }
}
