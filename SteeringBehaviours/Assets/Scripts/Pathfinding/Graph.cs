using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Node[] nodes;

    public Connection[] GetConnections(Node fromNode)
    {
        List<Connection> connections = new List<Connection>();

        foreach(Node neighbour in fromNode.neighbours)
        {
            Connection connection = new Connection(fromNode, neighbour);
            connections.Add(connection);
        }

        return connections.ToArray();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
