using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRecordStar
{
    private Node m_node;
    private Connection m_connection;
    private float m_costSoFar;
    private float m_estimatedTotalCost;

    public Node node
    {
        get { return m_node; }
        set { m_node = value; }
    }

    public Connection connection
    {
        get { return m_connection; }
        set { m_connection = value; }
    }

    public float costSoFar
    {
        get { return m_costSoFar; }
        set { m_costSoFar = value; }
    }

    public float estimatedTotalCost
    {
        get { return m_estimatedTotalCost; }
        set { m_estimatedTotalCost = value; }
    }
}
