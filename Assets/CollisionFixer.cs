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
            // Impulso hacia arriba (ajustá el valor según quieras)
            _rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }

}
