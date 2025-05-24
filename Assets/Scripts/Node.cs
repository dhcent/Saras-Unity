using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node cameFrom;
    public List<Node> connections;
    public bool Blocked;
    public float radius = 0.75f;
    private LayerMask obstacleLayer;

    //g score is distance from start to this
    public float gScore;
    //h score is distance from this to end
    public float hScore;

    public void Awake()
    {
        obstacleLayer = LayerMask.GetMask("Ground");   
    }

    public float FScore()
    {
        return gScore + hScore;
    }

    public bool getBlocked()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, obstacleLayer);
        if(colliders.Length > 0)
        {
            Blocked = true;
        }
        else{
            Blocked = false;
        }
        return Blocked;
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

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
