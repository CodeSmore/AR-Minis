using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ToppleBlocksUIController : MonoBehaviour
{
    // activate button activates, but only shows up when it's deactivated
    // noted b/c I had the wrong methods on each due to on/off naming previously x.x
    Button autoFireDeactivateButton, autoFireActivateButton;
    Button resetButton;
    Button setHeightButton;

    VisualElement setHeightUI, fireUI;

    Launcher cameraLauncher;
    
    TwoStepPlacementIndicator twoStepPlacementIndicator;
    UIDocument uiDocument;

    private void Awake()
    {
        cameraLauncher = FindObjectOfType<Launcher>();

        twoStepPlacementIndicator = FindObjectOfType<TwoStepPlacementIndicator>();
        uiDocument = GetComponent<UIDocument>();

        var root = GetComponent<UIDocument>().rootVisualElement;

        autoFireDeactivateButton = root.Q<Button>("auto-fire-button-on");
        autoFireActivateButton = root.Q<Button>("auto-fire-button-off");
        resetButton = root.Q<Button>("reset-button");

        setHeightButton = root.Q<Button>("set-height-button");

        setHeightUI = root.Q<VisualElement>("set-height-ui");
        fireUI = root.Q<VisualElement>("fire-ui");
    }

    private void Start()
    {
        autoFireDeactivateButton.clicked += DeactivateAutoFire;
        autoFireActivateButton.clicked += ActivateAutoFire;
        resetButton.clicked += ResetBlocks;

        setHeightButton.clicked += SetHeight;

        DisableVisualElements();
    }

    void ActivateAutoFire()
    {
        cameraLauncher.ToggleAutomaticFire(true);
        ToggleAutoFireButtons(true);
    }

    void DeactivateAutoFire() {
        cameraLauncher.ToggleAutomaticFire(false);
        ToggleAutoFireButtons(false);
    }

    void ToggleAutoFireButtons(bool activated)
    {
        autoFireActivateButton.visible = !activated;
        autoFireDeactivateButton.visible = activated;
    }

    void ResetBlocks()
    {
        ObjectPooler.Instance.DisableEntirePool("Ammo");

        twoStepPlacementIndicator.enabled = true;
        twoStepPlacementIndicator.ResetPrefab();
    }

    void SetHeight()
    {
        twoStepPlacementIndicator.SpawnPrefab();
        EnableFireUI();
    }

    public void EnableSetHightUI()
    {
        DisableVisualElements();
        setHeightUI.style.display = DisplayStyle.Flex;
    }

    public void EnableFireUI()
    {
        DisableVisualElements();
        fireUI.style.display = DisplayStyle.Flex;
    }

    void DisableVisualElements()
    {
        fireUI.style.display = DisplayStyle.None;
        setHeightUI.style.display = DisplayStyle.None;
    }
}
