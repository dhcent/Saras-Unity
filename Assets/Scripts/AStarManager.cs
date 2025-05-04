using System.Collections.Generic;
using UnityEngine;

public class AStarManager : MonoBehaviour
{
    //makes sure theres a static reference to this.
    public static AStarManager instance;

    public void Awake()
    {
        instance = this;
    }

    public List<Node> GeneratePath(Node start, Node end)
    {
        //list of all nodes able to view
        List<Node> openSet = new List<Node>();

        //set all gScores for each node to be max val
        foreach(Node n in FindObjectsByType<Node>(FindObjectsSortMode.None))
        {
            n.gScore = float.MaxValue;
        }

        //initialize start scores and add to list
        start.gScore = 0;
        start.hScore = Vector2.Distance(start.transform.position, end.transform.position);
        openSet.Add(start);

        while(openSet.Count > 0)
        {
            //find lowest fscore
            int lowestFIndex = default;
            for(int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].FScore() < openSet[lowestFIndex].FScore())
                {
                    lowestFIndex = i;
                }
            }
            //look at node with lowest fIndex
            Node currentNode = openSet[lowestFIndex];



        }


        return null;
    }
}
