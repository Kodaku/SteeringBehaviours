using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Heuristic
{
    public Node goalNode;

    public abstract float Estimate(Node fromNode);
}
