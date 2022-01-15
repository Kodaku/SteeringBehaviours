using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AStarAlgorithm
{
    public Graph graph;
    public Node startNode;
    public EuclideanHeuristic heuristic;
    
    public List<Connection> AStarPathFinding()
    {
        NodeRecordStar startRecord = new NodeRecordStar();
        startRecord.node = startNode;
        startRecord.connection = null;
        startRecord.costSoFar = 0.0f;
        startRecord.estimatedTotalCost = heuristic.Estimate(startNode);

        PathfindingListStar open = new PathfindingListStar();
        open.AddNodeRecord(startRecord);

        PathfindingListStar closed = new PathfindingListStar();

        NodeRecordStar current = startRecord;

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

                connection.fromNodeRecordStar = current;

                NodeRecordStar endNodeRecord = new NodeRecordStar();
                float endNodeHeuristic = 0.0f;

                if(closed.Contains(endNode))
                {
                    endNodeRecord = closed.Find(endNode);

                    if(endNodeRecord.costSoFar <= endNodeCost)
                    {
                        continue;
                    }

                    closed.Remove(endNodeRecord);

                    endNodeHeuristic = endNodeRecord.estimatedTotalCost - endNodeRecord.costSoFar;
                }
                else if(open.Contains(endNode))
                {
                    endNodeRecord = open.Find(endNode);

                    if(endNodeRecord.costSoFar <= endNodeCost)
                    {
                        continue;
                    }

                    endNodeHeuristic = endNodeRecord.estimatedTotalCost - endNodeRecord.costSoFar;
                }
                else
                {
                    endNodeRecord.node = endNode;
                    endNodeHeuristic = heuristic.Estimate(endNode);
                }

                endNodeRecord.costSoFar = endNodeCost;
                endNodeRecord.connection = connection;
                endNodeRecord.estimatedTotalCost = endNodeCost + endNodeHeuristic;

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
            return null;
        }
        else
        {
            List<Connection> path = new List<Connection>();

            while(current.node.name != startNode.name)
            {
                path.Add(current.connection);
                current = current.connection.fromNodeRecordStar;
            }

            path.Reverse();

            return path;
        }
    }
}
