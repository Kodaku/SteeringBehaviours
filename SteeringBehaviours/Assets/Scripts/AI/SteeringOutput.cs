using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringOutput
{
    private Vector3 m_linearAcceleration;
    private float m_angularAcceleration; // on y-axis

    public Vector3 linearAcceleration
    {
        get { return m_linearAcceleration; }
        set { m_linearAcceleration = value; }
    }

    public float angularAcceleration
    {
        get { return m_angularAcceleration; }
        set { m_angularAcceleration = value; }
    }
}
