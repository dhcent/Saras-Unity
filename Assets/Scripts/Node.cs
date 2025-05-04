using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node cameFrom;
    public List<Node> connections;

    //g score is distance from start to this
    public float gScore;
    //h score is distance from this to end
    public float hScore;

    public float FScore()
    {
        return gScore + hScore;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if(connections.Count > 0)
        {
            for(int i = 0; i < connections.Count; i++)
            {
                //draw a line to each of the neighbors
                Gizmos.DrawLine(transform.position, connections[i].transform.position);
            }
        }
    }
}
