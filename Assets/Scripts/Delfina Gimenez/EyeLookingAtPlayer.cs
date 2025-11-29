using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EyeLookingAtPlayer : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] float DistanceStartLook = 5f;
    [SerializeField] float rotateSpeed = 3f; 

    Material EyeMat;
    Vector3 RestLookDirection = default;
   public float LerpVal = 0f;

    void Start()
    {
        RestLookDirection = transform.position + transform.forward * 2f;
        EyeMat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float CurDist = Vector3.Distance(transform.position, Player.position);

      
        if (CurDist < DistanceStartLook)
        {
            LerpVal += Time.deltaTime;
            LerpVal = Mathf.Clamp01(LerpVal);
        }
        else
        {
            LerpVal -= Time.deltaTime / 2f;
            LerpVal = Mathf.Clamp01(LerpVal);
        }

       
        float PupilSize = Mathf.Lerp(0.2f, 1f, LerpVal);
        EyeMat.SetFloat("_EyeFocus", PupilSize);

     
        Vector3 targetLookPosition = Vector3.Lerp(RestLookDirection, Player.position, LerpVal);
        Vector3 direction = targetLookPosition - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
    }
}


