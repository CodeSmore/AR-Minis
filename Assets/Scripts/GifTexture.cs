using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GifTexture : MonoBehaviour
{
    [Space(10)]
    [SerializeField] float fps;
    public List<Texture> frames;

    Material mat;

    int index;

    void Awake()
    {
        mat = GetComponentInChildren<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        index = (int)(Time.time * fps);
        index = index % frames.Count;
        mat.mainTexture = frames[index];
    }
}
