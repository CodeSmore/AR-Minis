using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AtCameraPlacementController : MonoBehaviour
{
    [SerializeField] GameObject placementPrefab;
    [SerializeField] GameObject existingObject;

    [SerializeField] bool limitToSinglePlacement;

    GameObject placementIndicator;

    GameObject placedInstance;

    private void Awake()
    {
        placementIndicator = FindObjectOfType<AtCameraIndicator>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (placementPrefab != null)
                {
                    placedInstance = Instantiate(placementPrefab, placementIndicator.transform.position, placementIndicator.transform.rotation);
                    placedInstance.AddComponent<ARAnchor>();
                }
                else if (existingObject != null)
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

    public void ResetInstance()
    {
        if (placedInstance != null) {
            Vector3 prevPosition = placedInstance.transform.position;
            Quaternion prevRotation = placedInstance.transform.rotation;

            Destroy(placedInstance);

            placedInstance = Instantiate(placementPrefab, prevPosition, prevRotation);
        }

    }
}
