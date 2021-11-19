using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOptions : MonoBehaviour
{
    [SerializeField] List<Material> matOptions;

    MeshRenderer thisRenderer;

    private void Awake()
    {
        thisRenderer = GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // set default
        thisRenderer.material = matOptions[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchMaterials(int index)
    {
        thisRenderer.material = matOptions[index];
    }
}
