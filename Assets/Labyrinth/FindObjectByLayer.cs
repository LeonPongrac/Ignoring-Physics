using UnityEngine;

public class FindObjectsByLayer : MonoBehaviour
{
    public string targetLayer = "Side 4";

    void Start()
    {
        FindObjectsWithLayer(targetLayer);
    }

    void FindObjectsWithLayer(string layerName)
    {
        // Provjeri postoji li sloj
        int layer = LayerMask.NameToLayer(layerName);
        if (layer == -1)
        {
            Debug.LogError("Layer not found: " + layerName);
            return;
        }

        // Naði sve objekte koji su na tom sloju
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layer)
            {
                Debug.Log("Found object with layer " + layerName + ": " + obj.name);
            }
        }
    }
}
