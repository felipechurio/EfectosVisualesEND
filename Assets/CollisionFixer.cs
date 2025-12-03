using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionFixer : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pallet"))
        {
            _rb.AddForce(Vector3.up * 1f, ForceMode.Impulse);
        }
    }

}
