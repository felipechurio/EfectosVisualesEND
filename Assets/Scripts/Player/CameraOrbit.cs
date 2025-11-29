using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] Transform Follow;
    [SerializeField] float MaxDistance;

    private Vector2 Angle = new Vector2 (90 * Mathf.Deg2Rad, 0);

    [SerializeField] Vector2 Sensitivy;

    private Camera Camera;
    private Vector2 NearPlaneSize;

    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        Camera = GetComponent<Camera>();  
        
        CalculateNearPlaneSize();
    }

    private void CalculateNearPlaneSize() 
    {
        float Height = Mathf.Tan(Camera.fieldOfView * Mathf.Deg2Rad / 2) * Camera.nearClipPlane;
        float Width = Height * Camera.aspect;

        NearPlaneSize = new Vector2(Width, Height);
    }

    private Vector3[] GetCameraCollisionPoints(Vector3 Direction)
    {
        Vector3 Position = Follow.position;
        Vector3 Center = Position + Direction * (Camera.nearClipPlane + 10f);

        Vector3 Right = transform.right * NearPlaneSize.x;
        Vector3 Up = transform.up * NearPlaneSize.y;

        return new Vector3[] {Center - Right + Up,Center + Right + Up,Center - Right - Up,Center + Right -Up};
    }

    private void Update()
    {
        float Hor = Input.GetAxis("Mouse X"); 

        if (Hor != 0)
        {
            Angle.x += Hor * Mathf.Deg2Rad * Sensitivy.x;
        }

        float Ver = Input.GetAxis("Mouse Y");

        if (Ver != 0)
        {
            Angle.y += Ver * Mathf.Deg2Rad * Sensitivy.y;
            Angle.y = Mathf.Clamp(Angle.y, -80 * Mathf.Deg2Rad, 80); // Limit 

        } 
    }

    void LateUpdate()
    {
        Vector3 Direction = new Vector3(Mathf.Cos(Angle.x) * Mathf.Cos(Angle.y), -Mathf.Sin(Angle.y), -Mathf.Sin(Angle.x) * Mathf.Cos(Angle.y));

        RaycastHit Hit;

        float Distance = MaxDistance;

        Vector3[] Points = GetCameraCollisionPoints(Direction);

        foreach (Vector3 Point in Points)
        {
         if (Physics.Raycast(Follow.position, Direction, out Hit, MaxDistance))
         {
            Distance = Mathf.Min((Hit.point - Follow.position).magnitude, Distance);
         }
        }
       
        transform.position = Follow.position + Direction * Distance;
        transform.rotation = Quaternion.LookRotation(Follow.position - transform.position);
    }
}
