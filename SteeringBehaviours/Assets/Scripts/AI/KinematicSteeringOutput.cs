using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSteeringOutput
{
    private Vector3 m_direction;
    private float m_rotation;

    public Vector3 direction
    {
        get { return m_direction; }
        set { m_direction = value; }
    }

    public float rotation
    {
        get { return m_rotation; }
        set { m_rotation = value; }
    }
}
