using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager;
    private GameObject visual;

    private Label cameraForwardLabel, cameraBearingLabel, cameraPositionLabel, indicatorRotationLabel, rayHitRotationLabel;

    // Debug
    Vector3 hitEuler;

    //private void OnEnable()
    //{
    //    var rootVisualElement = FindObjectOfType<UIDocument>().rootVisualElement;

    //    cameraForwardLabel = rootVisualElement.Q<Label>("CameraForward");
    //    cameraBearingLabel = rootVisualElement.Q<Label>("CameraBearing");
    //    cameraPositionLabel = rootVisualElement.Q<Label>("CameraPosition");
    //    indicatorRotationLabel = rootVisualElement.Q<Label>("IndicatorRotation");
    //    rayHitRotationLabel = rootVisualElement.Q<Label>("RayHitRotation");
    //}

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
            //transform.position = hits[0].pose.position;
            //transform.rotation = hits[0].pose.rotation;

            // This works for HORIZONTAL
            //var cameraForward = Camera.main.transform.forward;
            //var cameraBearing = new Vector3(cameraForward.x, 0, Mathf.Abs(cameraForward.z)).normalized;
            //var indicatorRotation = Quaternion.LookRotation(cameraBearing);

            // This works (hopefully) for VERTICAL
            Quaternion indicatorRotation = hits[0].pose.rotation;
            //indicatorRotation.eulerAngles = new Vector3(
            //    90, 90, hits[0].pose.rotation.z + 90);
            indicatorRotation.eulerAngles = new Vector3(90, indicatorRotation.eulerAngles.y, indicatorRotation.eulerAngles.z);

            hitEuler = hits[0].pose.rotation.eulerAngles;

            Vector3 indicatorPosition = hits[0].pose.position;

            this.transform.SetPositionAndRotation(indicatorPosition, indicatorRotation);

            if (!visual.activeInHierarchy)
            {
                visual.SetActive(true);
            }

        }

        //UpdateDebugUI();
    }

    void UpdateDebugUI()
    {
        //cameraForwardLabel.text = $"Camera Forward: {cameraForward.ToString()}";
        //cameraBearingLabel.text = $"Camera Bearing: {cameraBearing.ToString()}";
        //cameraPositionLabel.text = $"Camera Position: {cameraPosition.ToString()}";
        indicatorRotationLabel.text = $"Indicator Rotation: {transform.rotation.eulerAngles.ToString()}";
        rayHitRotationLabel.text = $"RayHit Rotation: {hitEuler.ToString()}";
    }

    //private void OnGUI()
    //{
    //    var cameraForward = Camera.main.transform.forward;
    //    var cameraBearing = new Vector3(cameraForward.x, cameraForward.y, Mathf.Abs(cameraForward.z)).normalized;
    //    var indicatorRotation = Quaternion.LookRotation(cameraBearing);

    //    GUI.Box(new Rect(10, 10, 300, 300), "Vector Analysis!");
    //    GUI.Box(new Rect(40, 40, 250, 300), $"CameraForward: {cameraForward.ToString()}");
    //    GUI.Box(new Rect(40, 70, 250, 300), $"CameraBearing: {cameraBearing.ToString()}");
    //    GUI.Box(new Rect(40, 100, 250, 300), $"IndicatorRotation: {indicatorRotation.ToString()}");

    //}
}
