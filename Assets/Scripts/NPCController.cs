using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Node currentNode;
    public List<Node> path = new List<Node>();
    private Node[] nodes;

    public void Start()
    {
        nodes = FindObjectsByType<Node>(FindObjectsSortMode.None);  
    }
    public void IdlePathFind()
    {
        if(path != null && path.Count > 0)
        {

            Vector3 direction = (transform.position - path[0].transform.position).normalized;
            if(direction.x < 0)
            {
                transform.localScale = new Vector3(-1,1,1);
            }
            else
            {
                transform.localScale = new Vector3(1,1,1);
            }
            transform.position = Vector3.MoveTowards(
                transform.position, //from
                new Vector3(path[0].transform.position.x, path[0].transform.position.y, 0), //to
                2f * Time.deltaTime
                );
            //once distance is sufficiently close, choose next node on the path
            if(Vector2.Distance(transform.position, path[0].transform.position) < 0.01f)
            {
                //set currentnode to new node, and remove from path
                currentNode = path[0];
                path.RemoveAt(0);
            }   
        }
        else
        {
            CreateIdlePath();
        }
    }
    //for idle movement
    public void CreateIdlePath()
    {
        if (AStarManager.instance == null)
        {
            Debug.Log("AStarManager not initialized yet");
            return;
        }
        int rand = Random.Range(0,nodes.Length);
        path = AStarManager.instance.GeneratePath(currentNode, nodes[rand]);

        if (path == null)
        {
            Debug.LogWarning("GeneratePath returned null.");
        }
    }

    
}
