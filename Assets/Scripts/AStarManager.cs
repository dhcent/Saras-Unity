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

        //visited list
        List<Node> closedSet = new List<Node>();

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
            //default is 0
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
            closedSet.Add(currentNode);
            openSet.Remove(currentNode);

            //if node is end, then return path to get to node
            if(currentNode == end)
            {
                //Find path to get to end node.
                List<Node> path = new List<Node>();
                //note, path is in reverse order (end to start)
                path.Add(currentNode);
                while(currentNode != start)
                {
                    currentNode = currentNode.cameFrom;
                    path.Add(currentNode);
                }

                //flip path to normal order
                path.Reverse();
                return path;
            }

            //for each of node neighbors, reassign gScores
            //n refers to neighbor node
            foreach(Node n in currentNode.connections)
            {
                if(!n.getBlocked())
                {
                    float heldGScore = currentNode.gScore + Vector2.Distance(n.transform.position, end.transform.position);
                    if(heldGScore < n.gScore)
                    {
                        n.cameFrom = currentNode;
                        n.gScore = heldGScore;
                        n.hScore = Vector2.Distance(n.transform.position, end.transform.position);
                    }
                    //if not in open set already, add it. otherwise, just reassing the values.
                    if(!openSet.Contains(n) && !closedSet.Contains(n))
                    {
                        openSet.Add(n);
                    }
                }
            }
        }
        return null;
    }
}
