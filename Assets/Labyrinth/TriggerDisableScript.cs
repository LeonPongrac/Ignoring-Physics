using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDisableScript : MonoBehaviour
{
    public GameObject targetObject;
    public string scriptName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisableTargetScript();
        }
    }

    private void DisableTargetScript()
    {
        if (targetObject != null)
        {
            MonoBehaviour targetScript = targetObject.GetComponent(scriptName) as MonoBehaviour;
            if (targetScript != null)
            {
                targetScript.enabled = false;
            }
        }
    }
}
