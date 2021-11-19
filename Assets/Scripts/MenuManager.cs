using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Button button in buttons) {
            button.onClick.AddListener(delegate { LoadScene(button.name); });
        }
    }

    void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
