using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WalkSound : MonoBehaviour
{

    [SerializeField] AudioClip WalkSoundClip;

    AudioSource _AudioSource;

    PlayerController _PlayerController;

    bool IsWalking = true;

    // Start is called before the first frame update
    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();

        _PlayerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_PlayerController.HorizontalMove != 0 || _PlayerController.VerticalMove != 0)
        {
            if (IsWalking)
            {

                StartCoroutine(WalkTime());

                IsWalking = false;
            }
        } 
    }

    public IEnumerator WalkTime()
    {      
            if (_AudioSource.isPlaying == false)
            {

                _AudioSource.Play();

            } 

        yield return new WaitForSeconds(0.85f);

        IsWalking = true;

    }
}
