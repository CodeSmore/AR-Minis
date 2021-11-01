using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public EventType tileType;

    [SerializeField]
    Image tileImage;

    [SerializeField]
    GameObject[] edges;

    [SerializeField]
    private Color[] colorCodedEvents;

    private void Update()
    {
        
    }

    public void SetEventType(EventType eventType)
    {
        tileType = (EventType)eventType;
        tileImage.color = colorCodedEvents[(int)eventType];
    }

    public void SetEdges(char preDir, char nextDir)
    {
        List<int> edgesToDrop = new List<int>();

        // NOTE dropped edges are based on cyclical board
        // i.e. see 's','e', and 'x'
        if (preDir == 's')
        {
            edgesToDrop.Add(3);
        }
        else if (preDir == 'u')
        {
            edgesToDrop.Add(2);
        }
        else if (preDir == 'd')
        {
            edgesToDrop.Add(0);
        }
        else if (preDir == 'l')
        {
            edgesToDrop.Add(1);
        }
        else if (preDir == 'r')
        {
            edgesToDrop.Add(3);
        }

        if (nextDir == 'u')
        {
            edgesToDrop.Add(0);
        }
        else if (nextDir == 'd')
        {
            edgesToDrop.Add(2);
        }
        else if (nextDir == 'l')
        {
            edgesToDrop.Add(3);
        }
        else if (nextDir == 'r')
        {
            edgesToDrop.Add(1);
        }
        else if (nextDir == 'e')
        {
        }
        else if (nextDir == 'x')
        {
            edgesToDrop.Add(1);
        }

        DropEdges(edgesToDrop);
    }

    void DropEdges(List<int> edgesToDrop)
    {
        for (int i = 0; i < edgesToDrop.Count; ++i)
        {
            edges[edgesToDrop[i]].SetActive(false);
        }
    }
}
