using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyTransition : MonoBehaviour
{
    [SerializeField] float startingTransparency, endingTransparency;

    [Space(10)]
    [SerializeField] float transitionDuration;

    MeshRenderer thisRenderer;

    Material transparentMaterialInstance, opaqueMaterialInstance;

    bool transitioning, done;
    float timer;

    private void Awake()
    {
        thisRenderer = GetComponent<MeshRenderer>();
    }

    // each ammo is pooled, so make sure to reset values when enabled
    private void OnEnable()
    {
        transitioning = true;
        done = false;
        timer = 0;

        SetMaterials();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transitioning)
        {
            // transition alpha color
            Color currentColor = thisRenderer.material.color;

            thisRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b,
                Mathf.Lerp(startingTransparency, endingTransparency, timer / transitionDuration));
            timer += Time.deltaTime;

            transitioning = (timer <= transitionDuration);
        }
        else if (!done)
        {
            // set material from transparent to opaque so it creates shadows ;)
            thisRenderer.material = opaqueMaterialInstance;
            done = true;
        }
    }

    void SetMaterials()
    {
        if (transparentMaterialInstance == null || opaqueMaterialInstance == null)
        {
            transparentMaterialInstance = Instantiate(Resources.Load("Red Transparent") as Material);
            opaqueMaterialInstance = Instantiate(Resources.Load("Red Opaque") as Material);
        }

        thisRenderer.material = transparentMaterialInstance;
    }
}
