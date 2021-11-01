using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Created to avoid dragging and dropping hundreds of frames to the inspector :D

// 1. Folder of textures is placed in Resources
// 2a. Reference names using then names and padding
// 2b. Two or more potential Texture2D variables are used since post-fix names can be different
// 3. Input number of files and drag GifTexture attached to would-be prefab
// 4. RUN, then drag newly created gif object (one with GifTexture) to Prefabs folder

// NOTE: Material falls off child of GIF prefab for some reason, so just recreate a new one

public class CreateGIF : MonoBehaviour
{
    [SerializeField] int numFiles;
    [SerializeField] GifTexture gifToInitialize;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numFiles; ++i)
        {
            Texture2D texture1 = Resources.Load<Texture2D>("TrickOrTreat GIF/frame_" + i.ToString().PadLeft(3, '0') + "_delay-0.06s");
            Texture2D texture2 = Resources.Load<Texture2D>("TrickOrTreat GIF/frame_" + i.ToString().PadLeft(3, '0') + "_delay-0.07s");

            gifToInitialize.frames.Add((texture1 != null ? texture1 : texture2));
        }
    }
}
