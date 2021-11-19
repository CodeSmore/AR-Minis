using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SwipeManager))]
public class SwipeHandler : MonoBehaviour
{
    //public Player OurPlayer; // Perhaps your playerscript?
    SwipeManager swipeManager;
    TwoStepPlacementIndicator twoStepPlacementIndicator;

    [SerializeField] float changeCoefficient;

    private void Awake()
    {
        swipeManager = GetComponent<SwipeManager>();
        twoStepPlacementIndicator = FindObjectOfType<TwoStepPlacementIndicator>();
    }

    void Start()
    {
        //swipeManager.onSwipe += HandleSwipe;
        swipeManager.onFrameSwipe += HandleFrameSwipe;
        swipeManager.onLongPress += HandleLongPress;
    }

    // Handle swipes on a frame-by-frame basis
    void HandleFrameSwipe(SwipeAction swipeAction)
    {
        if (swipeAction.direction == SwipeDirection.Up)
        {
            float change = swipeAction.distance * changeCoefficient;

            twoStepPlacementIndicator.ChangeThreeDimenstionalVisualSize(change);

            // tell UIManager that swipe occured
            SwipeManager.InvokeOnSwipeDetected();
        }
        else if (swipeAction.direction == SwipeDirection.Down)
        {
            float change = swipeAction.distance * -changeCoefficient;

            twoStepPlacementIndicator.ChangeThreeDimenstionalVisualSize(change);

            // tell UIManager that swipe occured
            SwipeManager.InvokeOnSwipeDetected();
        }
    }

    // Handle full swipe, from initial press to release
    void HandleSwipe(SwipeAction swipeAction)
    {

    }

    void HandleLongPress(SwipeAction swipeAction)
    {
        //Debug.LogFormat("HandleLongPress: {0}", swipeAction);
    }
}