using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour
{
    [SerializeField] GameObject placementPrefab;
    [SerializeField] GameObject existingObject;

    [SerializeField] bool limitToSinglePlacement;

    GameObject placementIndicator;

    private void Awake()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                if (placementPrefab != null)
                {
                    Instantiate(placementPrefab, placementIndicator.transform.position, placementIndicator.transform.rotation);
                } else if (existingObject != null)
                {
                    existingObject.transform.SetPositionAndRotation(placementIndicator.transform.position, placementIndicator.transform.rotation);
                    existingObject.SetActive(true);
                }

                if (limitToSinglePlacement)
                {
                    placementIndicator.gameObject.SetActive(false);
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
