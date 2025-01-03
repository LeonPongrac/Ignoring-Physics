using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFollowingSphere : MonoBehaviour
{
    public GameObject targetObject;
    public Vector3 newPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RepositionObject();
        }
    }

    private void RepositionObject()
    {
        if (targetObject != null)
        {
            targetObject.transform.position = newPosition;
        }
    }
}
