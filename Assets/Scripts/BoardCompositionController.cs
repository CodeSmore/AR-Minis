using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoardTypes
{
    none,
    custom,
    battle,
    move,
    chance
}

public class BoardCompositionController : MonoBehaviour
{
    public BoardTypes boardType;
    private BoardTypes currentBoardType;

    public static BoardCompositionController instance;

    [SerializeField]
    private List<EventsPerBoard> boardComposition;

    // Initialization
    private void Awake()
    {
        instance = this;

        currentBoardType = BoardTypes.none;
    }

    // Start is called before the first frame update
    void Start()
    {
        // TODO
        // make sure percentages total 100
        // Options:
        // 1 - Add to/Remove from highest/lowest index ones til reach 100
        // 2 - Add to/Remove from each index, one by one, til reach 100

        if (currentBoardType == BoardTypes.none)
        {
            SetBoardComposition();
        }
    }

    void SetBoardComposition()
    {
        //currentBoardType = boardType;

        //EventType thisBoardsEvent;
        //if (currentBoardType == BoardTypes.battle)
        //{
        //    thisBoardsEvent = EventType.battle;
        //}
        //else if (currentBoardType == BoardTypes.move)
        //{
        //    thisBoardsEvent = EventType.move;
        //}
        //else if (currentBoardType == BoardTypes.chance)
        //{
        //    thisBoardsEvent = EventType.chance;
        //}
        //else
        //{
        //    return;
        //}

        //boardComposition = new List<EventsPerBoard>() { new EventsPerBoard(thisBoardsEvent, 100f) };
    }

    public List<EventsPerBoard> GetBoardComposition()
    {
        if (currentBoardType == BoardTypes.none)
        {
            SetBoardComposition();
        }
        return boardComposition;
    }
}

[Serializable]
public class EventsPerBoard
{
    public EventType theEvent;
    public float percentageOfBoard;

    public EventsPerBoard(EventType newEvent, float newPercentageOfBoard)
    {
        theEvent = newEvent;
        percentageOfBoard = newPercentageOfBoard;
    }
}
