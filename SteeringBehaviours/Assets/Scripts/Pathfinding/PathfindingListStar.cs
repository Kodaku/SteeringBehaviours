using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingListStar
{
    private List<NodeRecordStar> nodeRecords;

    public PathfindingListStar()
    {
        nodeRecords = new List<NodeRecordStar>();
    }

    public void AddNodeRecord(NodeRecordStar nodeRecord)
    {
        nodeRecords.Add(nodeRecord);
    }

    public NodeRecordStar GetSmallestElement()
    {
        NodeRecordStar smallestNode = nodeRecords[0];
        
        foreach(NodeRecordStar nodeRecord in nodeRecords)
        {   
            if(smallestNode.estimatedTotalCost > nodeRecord.estimatedTotalCost)
            {
                smallestNode = nodeRecord;
            }
        }

        return smallestNode;
    }

    public bool Contains(Node node)
    {
        foreach(NodeRecordStar nodeRecord in nodeRecords)
        {
            if(nodeRecord.node.name == node.name)
            {
                return true;
            }
        }

        return false;
    }

    public NodeRecordStar Find(Node node)
    {
        return nodeRecords.Find(currentNodeRecord => node.name == currentNodeRecord.node.name);
    }

    public void Remove(NodeRecordStar nodeRecord)
    {
        nodeRecords.Remove(nodeRecord);
    }

    public int length
    {
        get { return nodeRecords.Count; }
    }
}
