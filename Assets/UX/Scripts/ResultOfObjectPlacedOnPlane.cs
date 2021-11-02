using System;
using UnityEngine;

public class ResultOfObjectPlacedOnPlane : MonoBehaviour
{
    // Kept this b/c other methods attach themselves to onPlacedObject, which is a cool feature I've never seen!
    // ...but mostly here to avoid rewritting stuff

    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    public static event Action onPlacedObject;

    public static void InvokeOnPlacedObject()
    {
        onPlacedObject();
    }
}
