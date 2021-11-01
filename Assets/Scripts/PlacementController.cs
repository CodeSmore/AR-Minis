using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour
{
    [SerializeField] GameObject placementPrefab;

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
                Instantiate(placementPrefab, placementIndicator.transform.position, placementIndicator.transform.rotation);

                if (limitToSinglePlacement)
                {
                    placementIndicator.gameObject.SetActive(false);
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
