using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform Follow;
    [SerializeField] private LayerMask collisionMask; // capas para colisiones (excluir Player)

    [Header("Camera Settings")]
    [SerializeField] private float MaxDistance = 5f;
    [SerializeField] private Vector2 Sensitivity = new Vector2(3f, 3f);

    [Header("Smoothing")]
    [SerializeField] private float smoothSpeed = 10f;

    private Vector2 Angle = new Vector2(90 * Mathf.Deg2Rad, 0);
    private Camera cam;
    private Vector2 NearPlaneSize;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = GetComponent<Camera>();
        CalculateNearPlaneSize();
    }

    private void CalculateNearPlaneSize()
    {
        float height = Mathf.Tan(cam.fieldOfView * Mathf.Deg2Rad / 2f) * cam.nearClipPlane;
        float width = height * cam.aspect;
        NearPlaneSize = new Vector2(width, height);
    }

    private Vector3[] GetCameraCollisionPoints(Vector3 direction)
    {
        Vector3 pos = Follow.position;
        Vector3 center = pos + direction * (cam.nearClipPlane + 0.1f);

        Vector3 right = transform.right * NearPlaneSize.x;
        Vector3 up = transform.up * NearPlaneSize.y;

        return new Vector3[]
        {
            center - right + up,
            center + right + up,
            center - right - up,
            center + right - up
        };
    }

    private void Update()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        Angle.x += hor * Mathf.Deg2Rad * Sensitivity.x;
        Angle.y += ver * Mathf.Deg2Rad * Sensitivity.y;
        Angle.y = Mathf.Clamp(Angle.y, -80 * Mathf.Deg2Rad, 80 * Mathf.Deg2Rad);
    }

    private void LateUpdate()
    {
        // Calcular dirección según ángulos
        Vector3 direction = new Vector3(
            Mathf.Cos(Angle.x) * Mathf.Cos(Angle.y),
            -Mathf.Sin(Angle.y),
            -Mathf.Sin(Angle.x) * Mathf.Cos(Angle.y)
        );

        // Detectar colisiones
        float distance = MaxDistance;
        Vector3[] points = GetCameraCollisionPoints(direction);
        RaycastHit hit;

        foreach (Vector3 point in points)
        {
            if (Physics.Raycast(point, direction, out hit, MaxDistance, collisionMask))
            {
                distance = Mathf.Min(distance, (hit.point - Follow.position).magnitude);
            }
        }

        // Suavizado de posición
        Vector3 targetPos = Follow.position + direction * distance;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothSpeed);

        // Suavizado de rotación
        Quaternion targetRot = Quaternion.LookRotation(Follow.position - targetPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * smoothSpeed);
    }
}
