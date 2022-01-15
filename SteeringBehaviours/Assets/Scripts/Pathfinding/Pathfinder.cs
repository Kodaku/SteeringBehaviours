using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : Seek
{
    // public DijkstraAlgorithm dijkstraAlgorithm;
    public AStarAlgorithm aStarAlgorithm;
    private List<Connection> path;
    // Start is called before the first frame update
    void Start()
    {
        path = aStarAlgorithm.AStarPathFinding();
        foreach(Connection connection in path)
        {
            print("From Node " + connection.fromNode.name + " To Node: " + connection.toNode.name);
        }
        target = path[0].fromNode.node;
    }

    // Update is called once per frame
    void Update()
    {
        SteeringOutput steeringOutput = GetSteering();

        controller.Move(steeringOutput.linearAcceleration * Time.deltaTime);
    }

    public override SteeringOutput GetSteering()
    {
        // print(Vector3.Distance(transform.position, target.transform.position));
        if(Vector3.Distance(transform.position, target.transform.position) < 1.5f)
        {
            if(path.Count > 0)
            {
                print("Retargeting");
                target = path[0].toNode.node;
                path.Remove(path[0]);
            }
        }
        return base.GetSteering();
    }
}
