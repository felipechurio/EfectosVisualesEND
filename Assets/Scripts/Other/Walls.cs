using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    public GameObject[] ToActivate;
    public GameObject[] ToDeactivate;

    public GameObject[] objectsToActive; 

    private HashSet<GameObject> deactivatedObjects = new HashSet<GameObject>();

    public GameObject nextTextTrigger;
    public GameObject nextCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in ToActivate)
            {
                if (obj != null)
                    obj.SetActive(true);
            }

            foreach (GameObject obj in ToDeactivate)
            {
                if (obj != null)
                    obj.SetActive(false);
            }

            if (objectsToActive.Length > 0 && objectsToActive[0] != null && !deactivatedObjects.Contains(objectsToActive[0]))
            {
                objectsToActive[0].SetActive(true);
            }
        }
    }

    public void MarkAsDeactivated(GameObject obj)
    {
        if (obj != null && !deactivatedObjects.Contains(obj))
        {
            deactivatedObjects.Add(obj);
            ActivateNextObject(obj);

            if (objectsToActive.Length > 0 && obj == objectsToActive[objectsToActive.Length - 1])
            {
                foreach (GameObject go in ToActivate)
                {
                    if (go != null)
                        go.SetActive(false);
                }

                foreach (GameObject go in ToDeactivate)
                {
                    if (go != null)
                        go.SetActive(true);
                }

                if (nextTextTrigger != null)
                {
                    nextTextTrigger.SetActive(true);
                }

                if (nextCanvas != null)
                {
                    nextCanvas.SetActive(true);
                }

                gameObject.SetActive(false);
            }
        }
    }

    private void ActivateNextObject(GameObject currentObj)
    {
        for (int i = 0; i < objectsToActive.Length - 1; i++)
        {
            if (objectsToActive[i] == currentObj)
            {
                GameObject next = objectsToActive[i + 1];
                if (next != null && !deactivatedObjects.Contains(next))
                {
                    next.SetActive(true);
                }
                break;
            }
        }
    }
}
