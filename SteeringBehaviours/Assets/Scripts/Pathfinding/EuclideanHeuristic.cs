using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EuclideanHeuristic : Heuristic
{
    public override float Estimate(Node fromNode)
    {
        return Vector3.Distance(fromNode.node.transform.position, goalNode.node.transform.position);
    }
}
