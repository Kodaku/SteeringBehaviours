using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingList
{
    private List<NodeRecord> nodeRecords;

    public PathfindingList()
    {
        nodeRecords = new List<NodeRecord>();
    }

    public void AddNodeRecord(NodeRecord nodeRecord)
    {
        nodeRecords.Add(nodeRecord);
    }

    public NodeRecord GetSmallestElement()
    {
        NodeRecord smallestNode = nodeRecords[0];
        foreach(NodeRecord nodeRecord in nodeRecords)
        {   
            if(smallestNode.costSoFar > nodeRecord.costSoFar)
            {
                smallestNode = nodeRecord;
            }
        }

        return smallestNode;
    }

    public bool Contains(Node node)
    {
        foreach(NodeRecord nodeRecord in nodeRecords)
        {
            if(nodeRecord.node.name == node.name)
            {
                return true;
            }
        }

        return false;
    }

    public NodeRecord Find(Node node)
    {
        return nodeRecords.Find(currentNodeRecord => node.name == currentNodeRecord.node.name);
    }

    public void Remove(NodeRecord nodeRecord)
    {
        nodeRecords.Remove(nodeRecord);
    }

    public int length
    {
        get { return nodeRecords.Count; }
    }
}
