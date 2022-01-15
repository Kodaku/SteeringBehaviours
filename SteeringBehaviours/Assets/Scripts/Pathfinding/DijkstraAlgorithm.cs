using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DijkstraAlgorithm
{
    public Graph graph;
    public Node startNode;

    public List<Connection> PathfindDijkstra()
    {
        NodeRecord startRecord = new NodeRecord();
        startRecord.node = startNode;
        startRecord.connection = null;
        startRecord.costSoFar = 0.0f;

        PathfindingList open = new PathfindingList();
        open.AddNodeRecord(startRecord);

        PathfindingList closed = new PathfindingList();

        NodeRecord current = startRecord;

        while(open.length > 0)
        {
            current = open.GetSmallestElement();

            if(current.node.isGoal)
            {
                break;
            }

            Connection[] connections = graph.GetConnections(current.node);

            foreach(Connection connection in connections)
            {
                Node endNode = connection.toNode;
                float endNodeCost = current.costSoFar + connection.GetCost();

                connection.fromNodeRecord = current;

                NodeRecord endNodeRecord;

                if(closed.Contains(endNode))
                {
                    continue;
                }
                else if(open.Contains(endNode))
                {
                    endNodeRecord = open.Find(endNode);
                    if(endNodeRecord.costSoFar <= endNodeCost)
                    {
                        continue;
                    }
                }
                else
                {
                    endNodeRecord = new NodeRecord();
                    endNodeRecord.node = endNode;
                }

                endNodeRecord.costSoFar = endNodeCost;
                endNodeRecord.connection = connection;

                if(!open.Contains(endNode))
                {
                    open.AddNodeRecord(endNodeRecord);
                }
            }

            open.Remove(current);
            closed.AddNodeRecord(current);
        }

        if(!current.node.isGoal)
        {
            Debug.Log("No path was found");
            return null;
        }
        else
        {
            List<Connection> path = new List<Connection>();

            while(current.node.name != startNode.name)
            {
                path.Add(current.connection);
                current = current.connection.fromNodeRecord;
            }

            path.Reverse();

            return path;
        }
    }
}
