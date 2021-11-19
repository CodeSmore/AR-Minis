using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

// TODO refactor into PlacementController, PlacementIndicator, and/or VariablePlacementController
// options (HorizontalPlane vs VerticalPlane vs AtCamera) for how to handle indicator
public class AtCameraIndicator : MonoBehaviour
{
    [SerializeField] GameObject visual;
    [SerializeField] float indicatorPositionOffset;

    ARPlaneManager planeManager;
    bool initialized;


    private void Awake()
    {
        planeManager = FindObjectOfType<ARPlaneManager>();

        initialized = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        // hide placement indicator
        visual.SetActive(false);

        //planeManager.planesChanged += PlanesChanged;
    }

    //void PlanesChanged(ARPlanesChangedEventArgs args)
    //{
    //    if (!initialized) {
    //        initialized = true;
    //        planeManager.enabled = false;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        if (initialized) {
            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, Mathf.Abs(cameraForward.z)).normalized;
            var indicatorRotation = Quaternion.LookRotation(cameraBearing);

            Vector3 indicatorPosition = Camera.main.transform.position + (cameraForward * Camera.main.transform.parent.transform.localScale.x * indicatorPositionOffset);

            this.transform.SetPositionAndRotation(indicatorPosition, indicatorRotation);

            if (!visual.activeInHierarchy)
            {
                visual.SetActive(true);
            }
        }
    }
}
