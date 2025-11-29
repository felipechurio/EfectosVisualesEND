using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MultiPerspectiveCamera : MonoBehaviour
{
   
    [SerializeField] bool TPerson = true;

    [SerializeField] Transform TpTarget;
    [SerializeField] Transform FpTarget;

    [SerializeField] bool DisablePlayerMesh = true;

    [SerializeField] GameObject PlayerMesh;

    private Vector2 Angle = new Vector2(90 * Mathf.Deg2Rad, 0);
    private new Camera camera;
    private Vector2 NearPlaneSize;
    private Transform Follow;
    private float DefaultDistance;
    private float NewDistance;

    public float MaxDistace;
    public float MinDistance;

    public int ZoomVelocity;
    public float ZoomSmoth;
    public Vector2 Sensitivity = new Vector2(1, 1);

    public KeyCode KeyCode = KeyCode.C;


    void Start()
    {

        ChangePerspective(TPerson);


        DefaultDistance = (MaxDistace + MinDistance) / 2;
        NewDistance = DefaultDistance;

        Cursor.lockState = CursorLockMode.Locked;
        camera = GetComponent<Camera>();

        CalculateNearPlaneSize();
    }

    void ChangePerspective(bool ThirdPerson)
    {
        if (ThirdPerson)
        {
            Follow = TpTarget;

            if (DisablePlayerMesh)
            {
                PlayerMesh.SetActive(true);
            }
               
            TPerson = true;
        }
        else if (!ThirdPerson)
        {
            Follow = FpTarget;

            if (DisablePlayerMesh)
            {
                PlayerMesh.SetActive(false);
            }
                
            TPerson = false;

        }
    }

    private void CalculateNearPlaneSize()
    {
        float height = Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad / 2) * camera.nearClipPlane;
        float width = height * camera.aspect;

        NearPlaneSize = new Vector2(width, height);
    }

    private Vector3[] GetCameraCollisionPoints(Vector3 direction)
    {
        Vector3 position = Follow.position;
        Vector3 center = position + direction * (camera.nearClipPlane + 0.4f);

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

    void Update()
    {
        float hor = Input.GetAxis("Mouse X");

        if (hor != 0)
        {
            Angle.x += hor * Mathf.Deg2Rad * Sensitivity.x;
        }

        float ver = Input.GetAxis("Mouse Y");

        if (ver != 0)
        {
            Angle.y += ver * Mathf.Deg2Rad * Sensitivity.y;
            Angle.y = Mathf.Clamp(Angle.y, -80 * Mathf.Deg2Rad, 80 * Mathf.Deg2Rad);
        }

        if (TPerson)
        {

            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

            if (scrollDelta > 0)
            {
                NewDistance -= 0.1f * (Time.deltaTime * ZoomVelocity);
            }
            else if (scrollDelta < 0)
            {
                NewDistance += 0.1f * (Time.deltaTime * ZoomVelocity);
            }

            NewDistance = Mathf.Clamp(NewDistance, MinDistance, MaxDistace);
            DefaultDistance = Mathf.Lerp(DefaultDistance, NewDistance, ZoomSmoth);
        }
        else if (!TPerson)
        {
            DefaultDistance = 0.1f;


        }

        if (Input.GetKeyDown(KeyCode))
        {
            if (TPerson)
            {
                ChangePerspective(false);
            }

            else
            {
                ChangePerspective(true);
            }
              
        }
    }
 
    void LateUpdate()
    {
        Vector3 direction = new Vector3( Mathf.Cos(Angle.x) * Mathf.Cos(Angle.y),-Mathf.Sin(Angle.y),-Mathf.Sin(Angle.x) * Mathf.Cos(Angle.y));

        RaycastHit hit;

        float distance = DefaultDistance;

        Vector3[] points = GetCameraCollisionPoints(direction);

        foreach (Vector3 point in points)
        {
            if (Physics.Raycast(point, direction, out hit, DefaultDistance))
            {
                distance = Mathf.Min((hit.point - Follow.position).magnitude, distance);
            }
        }

        transform.position = Follow.position + direction * distance;
        transform.rotation = Quaternion.LookRotation(Follow.position - transform.position);
    }
}
