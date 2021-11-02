using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button GifDecorButton;

    void Awake()
    {
        GifDecorButton.onClick.AddListener(delegate { LoadScene(); });
    }

    void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
