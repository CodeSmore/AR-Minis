using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablePlacementController : MonoBehaviour
{
    [SerializeField] List<GameObject> placementPrefabs;

    GameObject placementIndicator;
    int placementPrefabIndex;

    private void Awake()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>().gameObject;
        placementPrefabIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Instantiate(placementPrefabs[placementPrefabIndex], placementIndicator.transform.position, placementIndicator.transform.rotation);

                // cycle through index if reached the final one, otherwise, increment
                placementPrefabIndex = (placementPrefabIndex >= placementPrefabs.Count - 1 ? 0 : ++placementPrefabIndex);

                // tells the coaching script, UIManager.cs, that a object was placed on a plane
                PlaceObjectsOnPlane.InvokeOnPlacedObject();
            }
        }
    }
}
