using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    public Node fromNode;
    public Node toNode;
    private NodeRecord m_fromNodeRecord;
    private NodeRecordStar m_fromNodeRecordStar;

    public NodeRecord fromNodeRecord
    {
        get { return m_fromNodeRecord; }
        set { m_fromNodeRecord = value; }
    }

    public NodeRecordStar fromNodeRecordStar
    {
        get { return m_fromNodeRecordStar; }
        set { m_fromNodeRecordStar = value; }
    }

    public Connection(Node fromNode, Node toNode)
    {
        this.fromNode = fromNode;
        this.toNode = toNode;
    }

    public float GetCost()
    {
        return Vector3.Distance(fromNode.node.transform.position, toNode.node.transform.position);
    }
}
