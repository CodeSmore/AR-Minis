using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TwoStepPlacementIndicator : MonoBehaviour
{
    [SerializeField] GameObject placementPrefab;
    [SerializeField] float defaultSizeCoefficient_3DVisual;

    ARRaycastManager rayManager;
    ToppleBlocksUIController toppleBlocksUIController;

    ARPlaneManager planeManager;
    ARPointCloudManager pointCloudManager;

    GameObject floorVisualInstance;
    GameObject instance_3DVisual;
    GameObject prefabInstance;

    // updated many times, so here to avoid redeclarations in Update()
    Vector3 indicatorPosition, cameraForward, cameraBearing;
    Quaternion indicatorRotation;

    float distanceToFloor;
    Vector3 scale_3DVisual, position_3DVisual;
    float sizeCoefficient_3DVisual;

    bool search;


    private void Awake()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        toppleBlocksUIController = FindObjectOfType<ToppleBlocksUIController>();

        planeManager = FindObjectOfType<ARPlaneManager>();
        pointCloudManager = FindObjectOfType<ARPointCloudManager>();

        floorVisualInstance = transform.GetChild(0).gameObject;
        instance_3DVisual = floorVisualInstance.transform.GetChild(0).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        // hide placement indicator
        floorVisualInstance.SetActive(false);
        instance_3DVisual.SetActive(false);

        search = true;

        // sets default size of 3DVisual
        sizeCoefficient_3DVisual = defaultSizeCoefficient_3DVisual;
    }

    // Update is called once per frame
    void Update()
    {
        // shoot raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        // if raycast hits an AR plane surface, update the postion and rotation
        if (hits.Count > 0 && search)
        {
            SetHorizontalVisualPositionAndRotation(hits);
        }

        // when the floorVisual is active...
        if (floorVisualInstance.activeInHierarchy)
        {
            // stop updating position of 2D visual indicator, and start the 3D portion
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && search)
            {
                search = false;

                EndSearchPhase();
                StartHeightSetPhase();
            }

            if (instance_3DVisual.activeSelf) {
                Set3DVisualPositionAndScale();
            }
        }
    }

    void EndSearchPhase()
    {
        // turn off plane creation
        // mostly for efficiency, once stuff is placed, new planes don't matter
        planeManager.enabled = false;
        // turn off point cloud creation & visuals
        // this is to remove the point cloud visuals and halt the creation of more
        pointCloudManager.enabled = false;

        foreach (var pointCloud in pointCloudManager.trackables)
        {
            pointCloud.gameObject.SetActive(false);
        }
    }

    void SetHorizontalVisualPositionAndRotation(List<ARRaycastHit> hits)
    {
        // This works for HORIZONTAL
        cameraForward = Camera.main.transform.forward;
        cameraBearing = new Vector3(cameraForward.x, 0, Mathf.Abs(cameraForward.z)).normalized;
        indicatorRotation = Quaternion.LookRotation(cameraBearing);

        indicatorPosition = hits[0].pose.position;

        this.transform.SetPositionAndRotation(indicatorPosition, indicatorRotation);

        if (!floorVisualInstance.activeInHierarchy)
        {
            floorVisualInstance.SetActive(true);
        }
    }

    void Set3DVisualPositionAndScale()
    {
        // update size of 3D vertical visual
        // set 3D visual Y scale to Y distance from camera to placement
        distanceToFloor = Mathf.Abs(Camera.main.transform.position.y - this.transform.position.y);

        scale_3DVisual = instance_3DVisual.transform.localScale;
        instance_3DVisual.transform.localScale = new Vector3(scale_3DVisual.x,
            (distanceToFloor / 2) * sizeCoefficient_3DVisual, scale_3DVisual.z);

        position_3DVisual = instance_3DVisual.transform.localPosition;
        instance_3DVisual.transform.localPosition = new Vector3(position_3DVisual.x,
            (distanceToFloor / 2) * sizeCoefficient_3DVisual, position_3DVisual.z);
    }

    void StartHeightSetPhase()
    {
        // get started with height
        toppleBlocksUIController.EnableSetHightUI();
        instance_3DVisual.SetActive(true);
        floorVisualInstance.GetComponent<MeshRenderer>().enabled = false;

        // tells the coaching script, UIManager.cs, that a object was placed on a plane
        PlaceObjectsOnPlane.InvokeOnPlacedObject();
    }

    public void ChangeThreeDimenstionalVisualSize(float change) {
        if (instance_3DVisual.activeSelf)
        {
            sizeCoefficient_3DVisual += change;
        }
    }

    public void SpawnPrefab()
    {
        Vector3 prefabPosition = new Vector3(this.transform.position.x,
                    this.transform.position.y + instance_3DVisual.transform.localScale.y * 2,
                    this.transform.position.z);
        prefabInstance = Instantiate(placementPrefab, prefabPosition, this.transform.rotation);

        instance_3DVisual.GetComponent<MaterialOptions>().SwitchMaterials(1);

        this.enabled = false;
    }

    public void ResetPrefab()
    {
        if (prefabInstance != null)
        {
            Destroy(prefabInstance);

            Vector3 prefabPosition = new Vector3(this.transform.position.x,
                this.transform.position.y + instance_3DVisual.transform.localScale.y * 2,
                this.transform.position.z);
            prefabInstance = Instantiate(placementPrefab, prefabPosition, this.transform.rotation);

        }

        this.enabled = false;
    }
}

