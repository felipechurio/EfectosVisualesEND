using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // lo unico que se agrego fue en la linea 57 el fixed delta time en la velocidad del player.

    public float HorizontalMove;
    public float VerticalMove;

    Rigidbody rb;

    [SerializeField] float PlayerSpeed = 350f;
    [SerializeField] GameObject Player;
    [SerializeField] AudioSource Switch;
    [SerializeField] Camera MainCamera;

    private Vector3 CamForward;
    private Vector3 CamRight;
    private float XRotation;

    public float mouseSensitivity = 100f;

    [HideInInspector] public float speedMultiplier = 1f; 

    void Start()
    {
        Switch.Play();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HorizontalMove = Input.GetAxisRaw("Horizontal");
        VerticalMove = Input.GetAxisRaw("Vertical");

        Vector3 camForward = MainCamera.transform.forward;
        Vector3 camRight = MainCamera.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = (camRight * HorizontalMove + camForward * VerticalMove).normalized;


        rb.velocity = move * (PlayerSpeed * speedMultiplier * Time.fixedDeltaTime);

        Player.transform.rotation = Quaternion.Euler(Player.transform.eulerAngles.x, MainCamera.transform.eulerAngles.y, Player.transform.eulerAngles.z);

        if (HorizontalMove != 0 && VerticalMove != 0)
        {
            Switch.Stop();
        }
    }

    public void CamDirection()
    {
        CamForward = MainCamera.transform.forward;
        CamRight = MainCamera.transform.right;

        CamForward.y = 0f;
        CamRight.y = 0f;

        CamForward = CamForward.normalized;
        CamRight = CamRight.normalized;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -60f, 60f);

        MainCamera.transform.localRotation = Quaternion.Euler(XRotation, 0f, 0f);
    }
}
