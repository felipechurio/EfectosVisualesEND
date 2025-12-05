using System.Collections;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform Follow;
    [SerializeField] private LayerMask collisionMask;

    [Header("Camera Settings")]
    [SerializeField] private float MaxDistance = 5f;
    [SerializeField] private Vector2 Sensitivity = new Vector2(3f, 3f);

    [Header("Smoothing")]
    [SerializeField] private float smoothSpeed = 12f;

    private Vector2 Angle = new Vector2(90 * Mathf.Deg2Rad, 0);
    private Camera cam;

    private float currentDistance;
    private float distanceVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = GetComponent<Camera>();
        currentDistance = MaxDistance;
    }

    void Update()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        Angle.x += hor * Sensitivity.x * Mathf.Deg2Rad;
        Angle.y += ver * Sensitivity.y * Mathf.Deg2Rad;

        Angle.y = Mathf.Clamp(Angle.y, -80 * Mathf.Deg2Rad, 80 * Mathf.Deg2Rad);
    }

    void LateUpdate()
    {
        Vector3 direction = new Vector3(
            Mathf.Cos(Angle.x) * Mathf.Cos(Angle.y),
            -Mathf.Sin(Angle.y),
            -Mathf.Sin(Angle.x) * Mathf.Cos(Angle.y)
        );

        RaycastHit hit;
        float targetDistance = MaxDistance;

        if (Physics.Raycast(Follow.position, direction, out hit, MaxDistance, collisionMask))
        {
            targetDistance = hit.distance;
        }

        currentDistance = Mathf.SmoothDamp(currentDistance, targetDistance, ref distanceVelocity, 0.05f);

        Vector3 finalPos = Follow.position + direction * currentDistance;
        transform.position = finalPos;

        transform.rotation = Quaternion.LookRotation(Follow.position - finalPos);
    }
}
