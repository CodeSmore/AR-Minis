using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class HorizontalPlacementIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager;
    private GameObject visual;

    private void Awake()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        // hide placement indicator
        visual.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // shoot raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        // if raycast hits an AR plane surface, update the postion and rotation
        if (hits.Count > 0)
        {
            // This works for HORIZONTAL
            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, Mathf.Abs(cameraForward.z)).normalized;
            var indicatorRotation = Quaternion.LookRotation(cameraBearing);

            // This works for VERTICAL
            //Quaternion indicatorRotation = hits[0].pose.rotation;
            //indicatorRotation.eulerAngles = new Vector3(90, indicatorRotation.eulerAngles.y, indicatorRotation.eulerAngles.z);

            Vector3 indicatorPosition = hits[0].pose.position;

            this.transform.SetPositionAndRotation(indicatorPosition, indicatorRotation);

            if (!visual.activeInHierarchy)
            {
                visual.SetActive(true);
            }

        }
    }
}

