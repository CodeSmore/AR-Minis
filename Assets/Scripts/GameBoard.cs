using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    BoardController boardController;

    private void Awake()
    {
        boardController = FindObjectOfType<BoardController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        boardController.BuildBoard(this.gameObject);
    }
}
